using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    Dictionary<eStageType, StageFramework> stageFrameworkDic;
    [SerializeField] eStageType _prevStage;
    [SerializeField] eStageType _currStage;
    bool isChanging;
    public bool LockStage;
    CancellationTokenSource mainProcessCts;
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
        stageFrameworkDic = new Dictionary<eStageType, StageFramework>((int)eStageType.End)
        {
            { eStageType.Normal,new NormalStageFramework2()},
        };
        IsLoad = true;
    }
    #endregion

    #region Stage Method
    //Setup Method
    public void SetupStage(eStageType type, long stageIndex)
    {
        if (isChanging) return;
        isChanging = true;

        if (mainProcessCts != null)
            mainProcessCts.Cancel();
        mainProcessCts = new CancellationTokenSource();

        _prevStage = _currStage;
        _currStage = type;

        SetupStageAsync(_currStage, stageIndex,mainProcessCts).Forget();
    }
    async UniTask SetupStageAsync(eStageType type, long stageIndex, CancellationTokenSource token)
    {
        try
        {
            await stageFrameworkDic[_currStage].SetupFrameworkAsync(stageIndex);

            while (LockStage)
                await UniTask.Yield(cancellationToken:token.Token);

            stageFrameworkDic[_currStage].StartFrameworkAsync(stageIndex).Forget();
        }
        catch (System.OperationCanceledException)
        {
            Debug.Log("SetupStageAsync was canceled.");
        }
        finally
        {
            token.Dispose();
            mainProcessCts = null;
            isChanging = false;
        }
    }
    //Stop Method
    public void StopStage()
    {
        stageFrameworkDic[_currStage].StopFramework();
    }
    public void ExitStage()
    {

    }
    #endregion

    #region Framework Method
    public T GetFramework<T>(eStageType type)where T:StageFramework
    {
        return stageFrameworkDic[type] as T;
    }
    #endregion
}
