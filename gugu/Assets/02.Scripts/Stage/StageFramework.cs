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
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);
        // var a = GameObject.Find("Actor").GetComponent<Actor>();
        BackgroundManager.Instance.ShowBackgroundByStage(2);
        //여기서 캐릭터 삽입 
        Player.PlayerCharacter = await SpawnManager.Instance.SpawnCharacter<Character>(1, Vector3.zero);
        await UniTask.Yield();
        Debug.Log("쥰비 온료");
    }
    public abstract UniTask StartStageAsync(long stageIndex);
}
