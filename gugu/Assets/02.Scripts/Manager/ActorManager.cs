using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorManager : TSingletonMono<ActorManager>
{
    //Ä³¸¯ÅÍ + ¿¡³×¹Ì¸¦ »ý¼ºÇÏ´Â ¾×ÅÍ ÆÑÅä¸® ..
    //¾ÆÀÌÅÛ ÆÑÅä¸®
    #region Fields
    //Factory
    ActorFactory actorFactory;

    //SpawnArea
    SpawnArea spawnArea;
    public SpawnArea SpawnArea => spawnArea;
    #endregion
    protected override void OnInitialize()
    {
        actorFactory = new ActorFactory(transform);
        CreateSpawnArea();
        IsLoad = true;

        
    }

    #region Spawn Method
    public async UniTask<Character> SpawnCharacter(long index, Vector3 position) => await actorFactory.SpawnObjectAsync(eActorType.Character, index, position) as Character;
    public async UniTask<Enemy> SpawnEnemy(long index, Vector3 position)  => await actorFactory.SpawnObjectAsync(eActorType.Enemy, index, position) as Enemy;
    public void RegisterActorPool(uint worldID, eActorType type, int pathHash) => actorFactory.RegisterToObjectPool(worldID, type, pathHash);
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
}
