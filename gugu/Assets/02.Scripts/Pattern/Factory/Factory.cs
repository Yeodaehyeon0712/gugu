using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T,TType> where T : PoolingObject where TType:System.Enum
{
    #region Fields
    Transform instanceRoot;
    uint currentWorldID;
    protected Dictionary<uint, T> spawnedObjectDic = new Dictionary<uint, T>();
    protected  Dictionary<TType ,Dictionary<int, MemoryPool<T>>> objectPool = new Dictionary<TType, Dictionary<int, MemoryPool<T>>>();
    #endregion

    #region Factory Method
    public Factory(Transform instanceRoot)
    {
        this.instanceRoot = instanceRoot;
        currentWorldID = 0;
        CreateObjectPoolDic();
    }
    #endregion

    #region Spawn Method
    public async UniTask<T> SpawnObjectAsync(TType type, long index, Vector2 position)
    {
        ++currentWorldID;
        uint snapshotID = currentWorldID;
        var resourceTuple = GetResourcePath(type, index);
        CheckObjectPool(type, resourceTuple.objectID);

        T spawnObject = GetFromObjectPool(type, resourceTuple.objectID);

        if(spawnObject==null)
        {
            T originalAsset = await DataManager.AddressableSystem.LoadAssetAsync<T>(resourceTuple.prefabPath);
            spawnObject = Object.Instantiate(originalAsset, instanceRoot);
            InitializeObject(spawnObject,type,index,resourceTuple.objectID);
        }
        ReSetObject(spawnObject,snapshotID , position);
        spawnedObjectDic.Add(snapshotID, spawnObject);
        return spawnObject;
    }
    protected abstract void InitializeObject(T obj,TType type,long index,int objectID);
    protected abstract void ReSetObject(T obj,uint worldID,Vector2 position);
    protected abstract (string prefabPath, int objectID) GetResourcePath(TType type,long index);

    #endregion

    #region Pooling Method
    protected abstract void CreateObjectPoolDic();
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
    }
    #endregion
}
