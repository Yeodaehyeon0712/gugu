using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : TSingletonMono<ActorManager>
{
    #region Fields
    //Factory Fields
    ActorFactory actorFactory;

    //Attack Handler Fields
    Queue<AttackHandler> attackHandlerQueue = new Queue<AttackHandler>(100);
    public AttackHandler PushAttackHandler
    {
        set => attackHandlerQueue.Enqueue(value);
    }

    //Spawn Area Fields
    SpawnArea spawnArea;
    public SpawnArea SpawnArea => spawnArea;
    #endregion

    #region Actor Manager Method
    protected override void OnInitialize()
    {
        actorFactory = new ActorFactory(transform);
        CreateSpawnArea();
        IsLoad = true;
    }
    private void Update()
    {
        if (TimeManager.Instance.IsActiveTimeFlow == false) return;
        OnUpdateAttackHandler();
    }
    #endregion

    #region Spawn Method
    public async UniTask<Character> SpawnCharacter(int objectID, Vector3 position) => await actorFactory.SpawnObjectAsync(eActorType.Character, objectID, position) as Character;
    public async UniTask<Enemy> SpawnEnemy(int objectID, Vector3 position)
    {   
        var enemy= await actorFactory.SpawnObjectAsync(eActorType.Enemy, objectID, position) as Enemy;
        enemy.ActiveActor();
        return enemy;
    }
    public void RegisterActorPool(uint worldID, eActorType type, int pathHash) => actorFactory.RegisterToObjectPool(worldID, type, pathHash);
    public Dictionary<uint, Actor> GetSpawnedActors => actorFactory.GetSpawnedObjects;
    #endregion

    #region Spawn Area Method
    void CreateSpawnArea()
    {
        spawnArea = Instantiate(Resources.Load<SpawnArea>("Background/SpawnArea"), transform);
        spawnArea.Initialize(GameConst.BgBlockSideSize);
    }
    public void RegisterSpawnAreaParent(Transform target)
    {
        spawnArea.RegisterParent(target != null ? target : transform);
    }
    public Vector3 GetRandomPosition()
    {
        return spawnArea.GetRandomPosition();
    }
    #endregion

    #region Attack Handler Method
    void OnUpdateAttackHandler()
    {
        while (attackHandlerQueue.Count > 0)
        {
            AttackHandler attackHandler = attackHandlerQueue.Dequeue();
            if (GetSpawnedActors.ContainsKey(attackHandler.TargetID)==false)
                continue;

            if (GetSpawnedActors[attackHandler.TargetID].ActorState != eActorState.Death)
            {
                if (attackHandler.Damage >= 0)
                    GetSpawnedActors[attackHandler.TargetID].Hit(in attackHandler);
                else
                    GetSpawnedActors[attackHandler.TargetID].Recovery(ref attackHandler);
            }
        }
    }
    #endregion
    public void Clear()
    {
        actorFactory.Clear();
    }
}
