using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> where T : Object
{
    #region Fields
    Transform instanceRoot;
    uint currentObjectID;
    protected Dictionary<uint, T> spawnedObjectDic = new Dictionary<uint, T>();
    protected  Dictionary<uint ,Dictionary<int, MemoryPool<T>>> objectPool = new Dictionary<uint, Dictionary<int, MemoryPool<T>>>();
    #endregion

    #region Factory Method
    public Factory(Transform instanceRoot)
    {
        this.instanceRoot = instanceRoot;
        currentObjectID = 0;
        CreateObjectPoolDic();
    }
    #endregion

    #region Spawn Method
    public async UniTask<T> SpawnObjectAsync(uint type, long index, Vector2 position)
    {
        ++currentObjectID;
        uint snapshotID = currentObjectID;
        var resourceTuple = GetResourcePath(type, index);
        CheckObjectPool(type, resourceTuple.pathHash);

        T spawnObject = GetFromObjectPool(type, resourceTuple.pathHash);

        if(spawnObject==null)
        {
            T originalAsset = await DataManager.AddressableSystem.LoadAssetAsync<T>(resourceTuple.prefabPath);
            spawnObject = Object.Instantiate(originalAsset, instanceRoot);
            InitializeObject(spawnObject,type,index,resourceTuple.pathHash);
        }
        ReSetObject(spawnObject , position);
        spawnedObjectDic.Add(snapshotID, spawnObject);
        return spawnObject;
    }
    protected abstract void InitializeObject(T obj,uint type,long index,int pathHash);
    protected abstract void ReSetObject(T obj,Vector2 position);
    protected abstract (string prefabPath, int pathHash) GetResourcePath(uint type,long index);

    #endregion

    #region Pooling Method
    protected abstract void CreateObjectPoolDic();
    protected abstract int GetPoolCapacity(uint type);

    void CheckObjectPool(uint type,int pathHash)
    {
        if (objectPool[type].TryGetValue(pathHash, out var memoryPool)) return;

        memoryPool = new MemoryPool<T>(GetPoolCapacity(type));
        objectPool[type][pathHash] = memoryPool;
    }
    T GetFromObjectPool(uint type, int pathHash)
    {
        return objectPool[type][pathHash].GetItem();
    }
    public void RegisterToObjectPool(uint targetObjectID, uint type, int pathHash)
    {
        if (spawnedObjectDic.ContainsKey(targetObjectID) == false) return;

        T obj = spawnedObjectDic[targetObjectID];
        spawnedObjectDic.Remove(targetObjectID);
        objectPool[type][pathHash].Register(obj);
    }
    #endregion
}
