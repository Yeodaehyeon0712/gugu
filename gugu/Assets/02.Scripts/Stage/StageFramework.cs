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
        Player.PlayerCharacter = actor;
        Player.RegisterSkill(DataManager.CharacterTable[Player.PlayerCharacter.Index].DefaultSkillKey);

        //Bg
        BackgroundManager.Instance.ShowBackgroundByStage(DataManager.StageTable[stageIndex].BackgroundPath);

        //UI
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);
        UIManager.Instance.ControllerUI.AddObserver(actor.Controller as CharacterControllerComponent);
        UIManager.Instance.FieldUI.SetHPBar(actor);

        //Camera
        CameraManager.Instance.RegisterFollowTarget(actor.transform);

        //Spawn Area
        ActorManager.Instance.RegisterSpawnAreaParent(actor.transform);
    }
    public async UniTask  StartFrameworkAsync(long stageIndex)
    {
        using (frameworkCTS = new CancellationTokenSource())
        {
            try
            {
                CurrentFrameworkState = eStageFrameworkState.InProgress;
                await ProcessFrameworkAsync(stageIndex, frameworkCTS.Token);
            }
            catch(System.OperationCanceledException)
            {
                CurrentFrameworkState = eStageFrameworkState.Cancel;
            }
            finally
            {
                if(frameworkCTS.IsCancellationRequested==false)
                    ShowResultUI();
            }
        }
    }
    protected abstract UniTask ProcessFrameworkAsync(long stageIndex, CancellationToken token);
    public void ShowResultUI()
    {
        TimeManager.Instance.IsActiveTimeFlow = false;
        //UIManager.Instance.
    }
    #endregion

    #region Stage Stop Method
    public void StopFramework()
    {
        if (frameworkState == eStageFrameworkState.InProgress)
            frameworkCTS.Cancel();

        CleanFrameworkAsync().Forget();
    }



    public async UniTask CleanFrameworkAsync()
    {
        await UniTask.WaitUntil(() => frameworkState != eStageFrameworkState.InProgress);
        CurrentFrameworkState = eStageFrameworkState.Clean;
        OnCleanFramework();//await ó�� ���ɼ� ����
        await ExitStage(() => Debug.Log("asd"), 3);
    }
    protected virtual void OnCleanFramework()
    {
        //Player
        Player.PlayerCharacter.Clean(0);
        Player.PlayerCharacter = null;

        //UI
        UIManager.Instance.FieldUI.FindFieldUI<FieldUI_HPBar>(eFieldUI.HPBar).Disable();//�̰͵� ����

        //Camera
        CameraManager.Instance.RegisterFollowTarget(null);

        //SpawnArea
        ActorManager.Instance.RegisterSpawnAreaParent(null);

    }
    public async UniTask ExitStage(UnityEngine.Events.UnityAction afterAction, float time)
    {
        await UniTask.WaitForSeconds(time);
        //������ ������ �� .
        afterAction?.Invoke();
    }
    #endregion
}
//To DO : Clean �Ͽ����� ��� ��ҵ��� ������� ó���Ѵ� .
//���� ���� �̵��Ͽ� ����â���� �̵��Ѵ� .

//To Do : �� ���º��� ���¸� �����Ѵ� . OnStateChange�� ..