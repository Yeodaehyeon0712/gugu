using Cysharp.Threading.Tasks;
using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMissile :BaseSkill
{
    public SkeletonMissile(long index, SkillData skillData) : base(index, skillData)
    {
    }

    protected override void OnUnRegister()
    {
        
    }

    protected override void OnLevelUp()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override async UniTask OnUsingSequenceAsync()
    {
        await UniTask.Yield();
    }

    protected override void OnRegister()
    {
        
    }
}
