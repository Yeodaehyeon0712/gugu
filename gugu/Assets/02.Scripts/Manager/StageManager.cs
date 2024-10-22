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
            { eStageType.Normal,new NormalStageFramework()},
        };
        IsLoad = true;
    }
    #endregion

    #region Stage Method
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
            await SetupFrameworkAsync(stageIndex);

            while (LockStage)
                await UniTask.Yield(cancellationToken:token.Token);

            await StartFrameworkAsync(stageIndex);
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
    async UniTask SetupFrameworkAsync(long index)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        await stageFrameworkDic[_currStage].SetupStageAsync(index);
        //FadeOn
        //해당 프레임 워크의 준비와 Clean을 실행한다 .
        //모든게 끝나면 FadeOut
    }
    async UniTask StartFrameworkAsync(long index)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        //해당 프레임 워크를 실행해준다 .
        //시간을 흐르게 하고 .. 각종 것들 리스타트..
        stageFrameworkDic[_currStage].StartStageAsync(index).Forget();
    }
    #endregion

    #region Framework Method
    public T GetFramework<T>(eStageType type)where T:StageFramework
    {
        return stageFrameworkDic[type] as T;
    }
    #endregion
}
