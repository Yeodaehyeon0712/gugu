using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class StageFramework
{
    #region Fields
    protected eStageResultState currentStageState;
    public eStageResultState CurrentStageStage 
    { 
        get => currentStageState; 
        set { currentStageState = value; OnStageChange();}
    }
    protected CancellationTokenSource frameworkCTS;

    void OnStageChange()
    {
        switch (currentStageState)
        {
            case eStageResultState.None:
                break;
            case eStageResultState.SetUp:
                break;
            case eStageResultState.InProgress:
                break;
            case eStageResultState.Victory:
                break;
            case eStageResultState.Defeat:
                break;
            case eStageResultState.Cancel:
                break;
        }
    }
    #endregion

    #region Stage Process Method
    public virtual async UniTask SetupStageAsync(long stageIndex)
    {
        CurrentStageStage = eStageResultState.SetUp;

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
    public async UniTask  StartStageFramework(long stageIndex)
    {
        using (frameworkCTS = new CancellationTokenSource())
        {
            try
            {
                CurrentStageStage = eStageResultState.InProgress;
                await FrameworkProcessAsync(stageIndex, frameworkCTS.Token);
            }
            catch(System.OperationCanceledException)
            {
                CurrentStageStage = eStageResultState.Cancel;
            }
            finally
            {
                if(frameworkCTS.IsCancellationRequested==false)
                    ShowResultUI();
            }
        }
    }
    protected abstract UniTask FrameworkProcessAsync(long stageIndex, CancellationToken token);
    public void ShowResultUI()
    {
        TimeManager.Instance.IsActiveTimeFlow = false;
        //UIManager.Instance.
    }
    #endregion

    #region Stage Stop Method
    public void StopStageFramework()
    {
        //await�� cts�� null�� �Ǵ°� ��ٸ��� �͵� ��� .
        if (currentStageState == eStageResultState.InProgress)
            frameworkCTS.Cancel();

        CleanStageFramework().Forget();
    }
    public async UniTask CleanStageFramework()
    {
        //Wait for all progress is Stop . this is for cancel Stage
        await UniTask.WaitUntil(() => currentStageState != eStageResultState.InProgress);

        OnCleanFramework();
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