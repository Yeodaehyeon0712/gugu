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
            spawnedActor.Initialize(type,resourceTuple.pathHash);
        }

        RefreshActor(spawnedActor, type, index, position);
        spawnedActor.Spawn(position);
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
                    break;
                }
        }

        if (pooledActorPool[type].TryGetValue(pathHash, out var memoryPool) == false)
        {
            memoryPool = new MemoryPool<Actor>(20);
            pooledActorPool[type][pathHash] = memoryPool;
        }
        return (resourcePath, animatorPath, pathHash);
    }
    void RefreshActor(Actor actor, eActorType type, long index, Vector2 position)
    {
        SetActorSkin(actor, type, index);
        SetActorStat(actor, type, index);
        spawnedActorDic.Add(currentActorID, actor);
    }
    void SetActorSkin(Actor actor, eActorType type, long index)
    {
        Animator animator = type switch
        {
            eActorType.Character => AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            eActorType.Enemy => AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            _ => null
        };
        actor.Skin.SetSkin(animator);
    }
    void SetActorStat(Actor actor, eActorType type, long index)
    {
        switch (type)
        {
            case eActorType.Character:
                {
                    var target = actor as Character;
                    target.Stat.SetStat(index);
                    break;
                }
            case eActorType.Enemy:
                {
                    //이때는 .. 스크립터블 오브젝트를 전달하자 .. 
                    break;
                }
        }
    }

    Data.CharacterData GetData(eActorType type, long index)
    {
        Data.CharacterData animator = type switch
        {
            eActorType.Character => DataManager.CharacterTable[index],
            eActorType.Enemy => DataManager.CharacterTable[index],
            _ => null
        };
        return animator;
    }
    #endregion
}
