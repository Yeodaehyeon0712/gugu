using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : TSingletonMono<EffectManager>
{
    EffectFactory effectFactory;

    protected override void OnInitialize()
    {
        effectFactory = new EffectFactory(transform);
        IsLoad = true;
    }
    #region Spawn Method
    public async UniTask<BaseEffect> SpawnEffect(eEffectType type,long index, Vector3 position,Transform parent=null) => await effectFactory.SpawnObjectAsync(type, index, position,parent);
    public void RegisterToEffectPool(uint worldID, eEffectType type, int pathHash) => effectFactory.RegisterToObjectPool(worldID, type, pathHash);
    public Dictionary<uint, BaseEffect> GetSpawnedActors => effectFactory.GetSpawnedObjects;
    #endregion

}
