using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class StageFramework
{
    #region Fields
    protected eStageResultState currentStageState;
    protected CancellationTokenSource frameworkCTS;
    #endregion

    #region Stage Method
    public virtual async UniTask SetupStageAsync(long stageIndex)
    {
        var actor = await SpawnManager.Instance.SpawnCharacter<Character>(1, Vector3.zero);
        Player.PlayerCharacter = actor;
        //배경
        BackgroundManager.Instance.ShowBackgroundByStage(DataManager.StageTable[stageIndex].BackgroundPath);
        //UI
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.BattleState);
        UIManager.Instance.ControllerUI.AddObserver(actor.Controller as CharacterControllerComponent);
        //음악

        //카메라
        CameraManager.Instance.RegisterFollowTarget(actor.transform);

        //Spawn
        SpawnManager.Instance.RegisterSpawnAreaParent(actor.transform);
        Debug.Log("쥰비 온료");
    }
    protected abstract UniTask FrameworkProcessAsync(long stageIndex, CancellationToken token);

    public async UniTask  StartStageFramework(long stageIndex)
    {
        using (frameworkCTS = new CancellationTokenSource())
        {
            try
            {
                await FrameworkProcessAsync(stageIndex, frameworkCTS.Token);
            }
            catch(System.OperationCanceledException)
            {
                Debug.Log("StageFramework was Canceld");
            }
            catch(System.Exception ex)
            {
                Debug.LogError($"Error during stage Framework: {ex.Message}");
            }
        }
    }
    public void StopStageFramework()
    {
        frameworkCTS.Cancel();

        //만약  필요하다면 상단에 bool 값으로 끝나는걸 체크 ..
    }
    public async UniTask ExitStage(UnityEngine.Events.UnityAction afterAction, float time)
    {
        await UniTask.WaitForSeconds(time);
        //씬으로 나가는 것 .
        afterAction?.Invoke();
    }
    #endregion
}
