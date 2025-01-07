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
    //???????? ?????? ..
    public async UniTask<BaseEffect> SpawnCharacter(long index, Vector3 position) => await effectFactory.SpawnObjectAsync(eEffectType.None, index, position);
}
