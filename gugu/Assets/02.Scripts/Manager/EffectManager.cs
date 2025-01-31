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
    public async UniTask<BaseEffect> SpawnEffect(long index, Vector3 position,Transform parent=null) => await effectFactory.SpawnObjectAsync(eEffectType.None, index, position,parent);
    public void RegisterToEffectPool(uint worldID, eEffectType type, int pathHash,bool resetParent=false) => effectFactory.RegisterToObjectPool(worldID, type, pathHash, resetParent);
    public Dictionary<uint, BaseEffect> GetSpawnedActors => effectFactory.GetSpawnedObjects;
    #endregion

}
