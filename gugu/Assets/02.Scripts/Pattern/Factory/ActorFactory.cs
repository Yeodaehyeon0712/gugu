using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory
{
    #region Fields
    Dictionary<eActorType , Dictionary<int,MemoryPool<Actor>>>pooledActorPool=new Dictionary<eActorType, Dictionary<int, MemoryPool<Actor>>>();
    Dictionary<uint, Actor> spawnedActorDic=new Dictionary<uint, Actor>();
    Transform instanceRoot;
    uint currentActorID;

    #endregion

    #region Factory Method
    public ActorFactory(Transform instanceRoot)
    {
        this.instanceRoot = instanceRoot;
        currentActorID = 0;
        for (eActorType i = eActorType.None; i < eActorType.End; ++i)
            pooledActorPool[i] = new Dictionary<int, MemoryPool<Actor>>();
    }
    #endregion

    #region Spawn Method
    public async UniTask<T> SpawnActorAsync<T>(eActorType type, long index, Vector2 position) where T : Actor
    {
        ++currentActorID;
        var resourceTuple = GetResourcePath(type, index);

        Actor spawnedActor = pooledActorPool[type][resourceTuple.pathHash].GetItem();

        if (spawnedActor == null)
        {
            T originAsset = await DataManager.AddressableSystem.LoadAssetAsync<T>(resourceTuple.prefabPath);
            spawnedActor = Object.Instantiate(originAsset, instanceRoot);
            spawnedActor.Initialize();
            //clonedAsset.SpawnHashCode = pathHash;
            spawnedActor.Skin.SetSkin(GetSkin(type, index));
        }

        spawnedActor.Spawn(position);
        spawnedActorDic.Add(currentActorID, spawnedActor);
        return spawnedActor as T;
    }
    (string prefabPath, string animatorPath, int pathHash) GetResourcePath(eActorType type, long index)
    {
        string prefabPath = null;
        string animatorPath = null;
        int pathHash = 0;

        switch (type)
        {
            case eActorType.Character:
                {
                    prefabPath = "Actor/Prefab";//클라이언트 콘스트에서 가져오자 ..
                    animatorPath = DataManager.CharacterTable[index].AnimatorPath;
                    pathHash = DataManager.CharacterTable[index].PathHash;
                    break;
                }
            case eActorType.Enemy:
                {
                    prefabPath = "Actor/Prefab";//클라이언트 콘스트에서 가져오자 ..
                    break;
                }
        }

        if (pooledActorPool[type].TryGetValue(pathHash, out var memoryPool)==false)
        {
            memoryPool = new MemoryPool<Actor>(20);
            pooledActorPool[type][pathHash] = memoryPool; 
        }
        return (prefabPath, animatorPath, pathHash);
    }  
    Animator GetSkin(eActorType type, long index)
    {
        Animator animator = type switch
        {
            eActorType.Character =>AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            eActorType.Enemy=>AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            _ =>null
        };
        return animator;
    }
    #endregion

}
