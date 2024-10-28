using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainScene : BaseScene
{
    public override void StartScene()
    {
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);
       // var a = GameObject.Find("Actor").GetComponent<Actor>();
        BackgroundManager.Instance.ShowBackgroundByStage(2);
        //a.Initialize();
        SpanwActor().Forget();

    }
    async UniTask SpanwActor()
    {
        await SpawnManager.Instance.SpawnCharacter<Character>(1, Vector3.zero);
        Debug.Log("소환 완료");
        StageManager.Instance.SetupStage(eStageType.Normal, 1);
    }


}
