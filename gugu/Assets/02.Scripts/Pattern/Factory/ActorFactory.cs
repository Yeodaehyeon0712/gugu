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
        CheckActorPool(type,resourceTuple.pathHash);

        Actor spawnedActor = pooledActorPool[type][resourceTuple.pathHash].GetItem();

        if (spawnedActor == null)
        {
            T originAsset = await DataManager.AddressableSystem.LoadAssetAsync<T>(resourceTuple.prefabPath);
            spawnedActor = Object.Instantiate(originAsset, instanceRoot);
            spawnedActor.Initialize(type,index,resourceTuple.pathHash);
        }

        RefreshActor(spawnedActor, type, index, position);
        return spawnedActor as T;
    }
    #endregion

    #region Spawn Support Method
    (string prefabPath, string animatorPath, int pathHash) GetResourcePath(eActorType type, long index)
    {
        string resourcePath = null;
        string animatorPath = null;
        int pathHash = 0;

        switch (type)
        {
            case eActorType.Character:
                {
                    var table = DataManager.CharacterTable[index];
                    resourcePath = table.ResourcePath;
                    animatorPath = table.AnimatorPath;
                    pathHash = table.PathHash;
                    break;
                }
            case eActorType.Enemy:
                {
                    var table = DataManager.EnemyTable[index];
                    resourcePath = table.ResourcePath;
                    animatorPath = table.AnimatorPath;
                    pathHash = table.PathHash;
                    break;
                }
        }
        return (resourcePath, animatorPath, pathHash);
    }
    void CheckActorPool(eActorType type,int pathHash)
    {
        if (pooledActorPool[type].TryGetValue(pathHash, out var memoryPool) == false)
        {
            int capacity = type switch
            {
                eActorType.Character => 5,
                eActorType.Enemy=>100,
                _ => 0,
            };
            memoryPool = new MemoryPool<Actor>(capacity);
            pooledActorPool[type][pathHash] = memoryPool;
        }
    }
    void RefreshActor(Actor actor, eActorType type, long index, Vector2 position)
    {
        RefreshActorSkin(actor, type, index);
        RefreshActorStat(actor, type, index);

        spawnedActorDic.Add(currentActorID, actor);
        actor.Spawn(position);
    }
    void RefreshActorSkin(Actor actor, eActorType type, long index)
    {
        RuntimeAnimatorController animator = type switch
        {
            eActorType.Character => AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            eActorType.Enemy => AddressableSystem.GetAnimator(DataManager.EnemyTable[index].AnimatorPath),
            _ => null
        };
        actor.Skin.SetSkin(animator);
    }
    void RefreshActorStat(Actor actor, eActorType type, long index)
    {
        switch (type)
        {
            case eActorType.Character:
                {
                    var target = actor as Character;
                    target.Stat.SetStat(DataManager.CharacterTable[index]);
                    break;
                }
            case eActorType.Enemy:
                {
                    //이때는 .. 스크립터블 오브젝트를 전달하자 .. 
                    break;
                }
        }
    }
    #endregion
}
