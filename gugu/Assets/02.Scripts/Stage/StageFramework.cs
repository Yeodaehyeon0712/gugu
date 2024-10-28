using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public abstract class StageFramework
{
    //Ÿ�̸� �޼��� .. .
    //Ŭ��
    //Exit
    public virtual async UniTask SetupStageAsync(long stageIndex)
    {
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);
        // var a = GameObject.Find("Actor").GetComponent<Actor>();
        BackgroundManager.Instance.ShowBackgroundByStage(2);
        //���⼭ ĳ���� ���� 
        Player.PlayerCharacter = await SpawnManager.Instance.SpawnCharacter<Character>(1, Vector3.zero);
        await UniTask.Yield();
        Debug.Log("��� �·�");
    }
    public abstract UniTask StartStageAsync(long stageIndex);
}
