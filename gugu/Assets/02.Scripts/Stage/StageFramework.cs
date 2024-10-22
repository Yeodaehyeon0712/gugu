using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class StageFramework
{
    //타이머 메서드 .. .
    //클린
    //Exit
    public virtual async UniTask SetupStageAsync(long stageIndex)
    {
        await UniTask.Yield();
    }
    public abstract UniTask StartStageAsync(long stageIndex);
}
