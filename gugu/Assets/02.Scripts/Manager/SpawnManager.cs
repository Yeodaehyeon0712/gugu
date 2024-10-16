using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : TSingletonMono<SpawnManager>
{
    //ĳ���� + ���׹̸� �����ϴ� ���� ���丮 ..
    //������ ���丮
    #region Fields
    ActorFactory actorFactory;
    #endregion
    protected override void OnInitialize()
    {
        actorFactory = new ActorFactory(transform);
        IsLoad = true;
    }

    #region Spawn Method
    public async UniTask<T> SpawnCharacter<T>(long index, Vector3 position) where T : Actor => await actorFactory.SpawnActorAsync<T>(eActorType.Character, index, position);
    public async UniTask<T> SpawnEnemy<T>(long index, Vector3 position) where T : Actor => await actorFactory.SpawnActorAsync<T>(eActorType.Enemy, index, position);

    #endregion
}
