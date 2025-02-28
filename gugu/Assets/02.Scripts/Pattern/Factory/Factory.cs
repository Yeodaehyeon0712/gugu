using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T,TType> where T : PoolingObject<TType> where TType:System.Enum
{
    #region Fields
    protected  Dictionary<TType ,Dictionary<int, MemoryPool<T>>> objectPool = new Dictionary<TType, Dictionary<int, MemoryPool<T>>>();
    public Dictionary<uint, T> GetSpawnedObjects => spawnedObjectDic;
    protected Dictionary<uint, T> spawnedObjectDic = new Dictionary<uint, T>();

    Transform instanceRoot;
    uint currentWorldID;
    #endregion

    #region Factory Method
    public Factory(Transform instanceRoot)
    {
        this.instanceRoot = instanceRoot;
        currentWorldID = 0;
        CreateObjectPool();
    }
    void CreateObjectPool()
    {
        foreach (TType type in System.Enum.GetValues(typeof(TType)))
        {
            objectPool[type] = new Dictionary<int, MemoryPool<T>>();
        }
    }
    #endregion

    #region Spawn Method
    public async UniTask<T> SpawnObjectAsync(TType type,int objectID, Vector2 position,Transform parent=null)
    {
        ++currentWorldID;
        uint snapshotID = currentWorldID;
        CheckObjectPool(type, objectID);

        T spawnObject = GetFromObjectPool(type, objectID);

        if(spawnObject==null)
        {
            T originalAsset = await DataManager.AddressableSystem.LoadAssetAsync<T>(GetResourcePath(type, objectID));
            spawnObject = Object.Instantiate(originalAsset, instanceRoot);
            InitializeObject(spawnObject,type,objectID);
        }
        if (parent != null) 
            spawnObject.SetParent(parent);

        SpawnObject(spawnObject,snapshotID , position);
        spawnedObjectDic.Add(snapshotID, spawnObject);
        return spawnObject;
    }
    protected abstract void InitializeObject(T obj,TType type,int objectID);
    protected abstract void SpawnObject(T obj,uint worldID,Vector2 position);
    protected abstract string GetResourcePath(TType type,int objectID);

    #endregion

    #region Pooling Method

    protected abstract int GetPoolCapacity(TType type);

    void CheckObjectPool(TType type,int objectID)
    {
        if (objectPool[type].TryGetValue(objectID, out var memoryPool)) return;

        memoryPool = new MemoryPool<T>(GetPoolCapacity(type));
        objectPool[type][objectID] = memoryPool;
    }
    T GetFromObjectPool(TType type, int objectID)
    {
        return objectPool[type][objectID].GetItem();
    }
    public void RegisterToObjectPool(uint targetWorldID, TType type, int objectID)
    {
        if (spawnedObjectDic.ContainsKey(targetWorldID) == false) return;

        T obj = spawnedObjectDic[targetWorldID];
        spawnedObjectDic.Remove(targetWorldID);
        objectPool[type][objectID].Register(obj);

        if (obj.ResetParent == true)
            obj.transform.SetParent(instanceRoot);
    }
    public void Clear()
    {
        foreach (var obj in spawnedObjectDic)
        {
            obj.Value.Clean(0);
        }
        spawnedObjectDic.Clear();
    }
    #endregion
}
