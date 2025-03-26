using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class StageFramework
{
    #region Fields
    protected eStageFrameworkState frameworkState;
    public eStageFrameworkState CurrentFrameworkState 
    { 
        get => frameworkState; 
        set { frameworkState = value; OnStageChange();}
    }
    protected CancellationTokenSource frameworkCTS;

    void OnStageChange()
    {
        switch (frameworkState)
        {
            case eStageFrameworkState.None:
                break;
            case eStageFrameworkState.SetUp:
                break;
            case eStageFrameworkState.InProgress:
                break;
            case eStageFrameworkState.Victory:
                break;
            case eStageFrameworkState.Defeat:
                break;
            case eStageFrameworkState.Clean:
                break;
        }
    }
    #endregion

    #region Stage Process Method
    public virtual async UniTask SetupFrameworkAsync(long stageIndex)
    {
        CurrentFrameworkState = eStageFrameworkState.SetUp;

        //Actor
        var actor = await ActorManager.Instance.SpawnCharacter(1, Vector3.zero);
        Player.RegisterPlayer(actor);

        //Bg
        BackgroundManager.Instance.ShowBackgroundByStage(DataManager.StageTable[stageIndex].BackgroundPath);

        //UI
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);        
    }
    public async UniTask  StartFrameworkAsync(long stageIndex)
    {
        using (frameworkCTS = new CancellationTokenSource())
        {
            try
            {
                CurrentFrameworkState = eStageFrameworkState.InProgress;
                Player.PlayerCharacter.ActiveActor();
                await ProcessFrameworkAsync(stageIndex, frameworkCTS.Token);
            }
            catch(System.OperationCanceledException)
            {
                CurrentFrameworkState = eStageFrameworkState.Cancel;
            }
            finally
            {
                if (frameworkCTS.IsCancellationRequested == false)
                    UIManager.Instance.ResultPopUpUI.Enable();
            }
        }
    }
    protected abstract UniTask ProcessFrameworkAsync(long stageIndex, CancellationToken token);
    #endregion

    #region Stage Stop Method
    public void StopFramework(bool skipResult)
    {
        if (frameworkState == eStageFrameworkState.InProgress)
            frameworkCTS.Cancel();

        StopFrameworkAsync(skipResult).Forget();
    }
    async UniTask StopFrameworkAsync(bool skipResult)
    {
        await UniTask.WaitUntil(() => frameworkState != eStageFrameworkState.InProgress);
        OnStopFramework();

        if (skipResult)
            CleanFramework();
        else
            UIManager.Instance.ResultPopUpUI.Enable();
    }
    //Remove Dynamic Things
    protected virtual void OnStopFramework()
    {
        CurrentFrameworkState = eStageFrameworkState.Defeat;
        //Player
        Player.UnRegisterPlayer();
        ActorManager.Instance.Clear();
    }
    #endregion

    #region Stage Clean Method
    public void CleanFramework()
    {
        CurrentFrameworkState = eStageFrameworkState.Clean;
        OnCleanFramework();
        //스테이지 나가기 까지 처리
    }
    //Remove Static Things
    protected virtual void OnCleanFramework()
    {
        //BG
        BackgroundManager.Instance.HideBackground();
        //UI
        UIManager.Instance.GameUI.CloseUIByFlag(eUI.Controller | eUI.BattleState);
        Debug.Log("클린 완료");
    }
    async UniTask ExitStage(UnityEngine.Events.UnityAction afterAction, float time)
    {
        await UniTask.WaitForSeconds(time);
        //씬으로 나가는 것 .
        afterAction?.Invoke();
    }
    #endregion
}
