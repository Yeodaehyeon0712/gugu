using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : TSingletonMono<SpawnManager>
{
    //캐릭터 + 에네미를 생성하는 액터 팩토리 ..
    //아이템 팩토리
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
    public async UniTask<T> SpawnCharacter<T>(long index, Vector3 position) where T : Actor => await actorFactory.SpawnActorAsync<T>(eActorType.Character, index, position);
    public async UniTask<T> SpawnEnemy<T>(long index, Vector3 position) where T : Actor => await actorFactory.SpawnActorAsync<T>(eActorType.Enemy, index, position);
    public void RegisterActorPool(uint worldID) => actorFactory.RegisterActorPool(worldID);
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
