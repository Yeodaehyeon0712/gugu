using Firebase.Extensions;
using RobinBird.FirebaseTools.Storage.Addressables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using Cysharp.Threading.Tasks;


public class AddressableSystem
{
    #region Fields
    eAddressableState state;
    public bool IsLoad;
    Dictionary<string, GameObject> assetResourceContainer = new Dictionary<string, GameObject>();
    Dictionary<string, TextAsset> tableContainer = new Dictionary<string, TextAsset>();
    Dictionary<string, AnimatorContainer> animatorContainer = new Dictionary<string, AnimatorContainer>();
    #endregion

    public void Initialize()
    {
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageAssetBundleProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageJsonAssetProvider());
        Addressables.ResourceManager.ResourceProviders.Add(new FirebaseStorageHashProvider());
        Addressables.InternalIdTransformFunc += FirebaseAddressablesCache.IdTransformFunc;

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                FirebaseAddressablesManager.IsFirebaseSetupFinished = true;
                state = eAddressableState.Initialized;
                LoadAsync().Forget();
            }
            else
                UnityEngine.Debug.LogError(System.String.Format(
                    "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
        });
    }

    #region Addressable Method
    async UniTask LoadAsync()
    {
        try
        {
            #region Cache Method
            //var cachePaths = new List<string>();
            //Caching.GetAllCachePaths(cachePaths);
            //foreach (var cachePath in cachePaths)
            //{
            //    Debug.Log($"Cache path: {cachePath}");
            //}
            //Caching.ClearCache();
            #endregion
            await CatalogCheckAsync();

            if (state == eAddressableState.FindPatch)
                await DownloadAssetAsync();
            else
                await LoadMemoryAsync();

            state = eAddressableState.Complete;
            IsLoad = true;
        }
        catch(System.Exception ex)
        {
            Debug.LogError($"An error occurred during the LoadAsync: {ex.Message}");
        }
    }
    async UniTask CatalogCheckAsync()
    {
        try
        {
            AsyncOperationHandle<List<string>> checkForUpdateHandle = Addressables.CheckForCatalogUpdates();
            await checkForUpdateHandle.ToUniTask();

            List<string> catalogsToUpdate = checkForUpdateHandle.Result;
            if (catalogsToUpdate.Count > 0)
            {
                AsyncOperationHandle<List<IResourceLocator>> updateHandle = Addressables.UpdateCatalogs(catalogsToUpdate, true);
                await updateHandle.ToUniTask();
                state = eAddressableState.FindPatch;
            }
            else
                state = eAddressableState.LoadMemory;
        }
        catch(System.Exception ex)
        {
            Debug.LogError($"An error occurred during the catalog check: {ex.Message}");
        }   
    }

    async UniTask DownloadAssetAsync()
    {
        AsyncOperationHandle operationHandle = Addressables.DownloadDependenciesAsync("default");
        state = eAddressableState.DownloadDependencies;
        await operationHandle.ToUniTask();
        AssetBundle.UnloadAllAssetBundles(true);
        Addressables.Release(operationHandle);
        await LoadMemoryAsync();    
    }
    async UniTask LoadMemoryAsync()
    {
        state = eAddressableState.LoadMemory;
        AsyncOperationHandle<IResourceLocator> locatorHandle = Addressables.InitializeAsync(true);
        await locatorHandle.ToUniTask();

        await LoadAssetAsync(eAddressableState.TableMemory,tableContainer);
        await LoadAssetAsync(eAddressableState.AnimatorMemory, animatorContainer);

        state = eAddressableState.Complete;
    }
    
    async UniTask LoadAssetAsync<T>(eAddressableState state, Dictionary<string, T> container)
    {
        if (state <= eAddressableState.LoadMemory||state>=eAddressableState.Complete)
        {
            Debug.LogError($"This state: {state} is not a valid load asset state. Check again.");
            return;
        }
        this.state = state;
        string key = state.ToString().Replace("Memory", string.Empty).Trim();
        AsyncOperationHandle<IList<IResourceLocation>> locationListHandle = Addressables.LoadResourceLocationsAsync(key, null);
        await locationListHandle.ToUniTask();

        List<UniTask> loadingTasks = new List<UniTask>();
        foreach (var location in locationListHandle.Result)
        {
            if (location.ResourceType != typeof(T))
            {
                continue;
            }

            var loadTask = Addressables.LoadAssetAsync<T>(location.PrimaryKey).ToUniTask().ContinueWith(task =>
            {
                container.Add(location.PrimaryKey, task);
            });

            loadingTasks.Add(loadTask);
        }

        await UniTask.WhenAll(loadingTasks);
        Addressables.Release(locationListHandle);
    }
    #endregion

    #region Get
    public static TextAsset GetTable(string key)
    {
        if (DataManager.AddressableSystem.tableContainer.TryGetValue(key, out var database))
        {
            return database;
        }

        Debug.LogError($"Database with resource path {key} not found in the database container.");
        return null;
    }
    public static RuntimeAnimatorController GetAnimator(string key)
    {
        if (DataManager.AddressableSystem.animatorContainer.TryGetValue(key, out var animatorContainer))
        {
            return animatorContainer.Animator;
        }

        Debug.LogError($"Animator with resource path {key} not found in the animator container.");
        return null;
    }
    #endregion

    #region Load Asset Async Method
    public async UniTask<T> LoadAssetAsync<T>(string path) where T : Object
    {
        try
        {
            if (assetResourceContainer.TryGetValue(path, out var cachedAsset))
            {
                return cachedAsset.GetComponent<T>();
            }

            var asyncHandle = Addressables.LoadAssetAsync<GameObject>(path);
            await asyncHandle.ToUniTask();

            if (asyncHandle.Status == AsyncOperationStatus.Succeeded)
            {
                var loadedAsset = asyncHandle.Result;
                assetResourceContainer[path] = loadedAsset;
                return loadedAsset.GetComponent<T>();
            }
            else
            {
                throw new System.Exception($"Failed to load asset at path: {path}. Status: {asyncHandle.Status}");
            }

        }
        catch  (System.Exception ex)
        {
            Debug.LogError($"Error while loading asset at path: {path}. Exception: {ex.Message}");
            return null; 
        }
    
    }
    #endregion

}
