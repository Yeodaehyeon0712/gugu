using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class StageManager : TSingletonMono<StageManager>
{
    #region Fields
    [SerializeField] eStageType _prevStage;
    [SerializeField] eStageType _currStage;
    bool isChanging;
    public bool LockStage;
    CancellationTokenSource mainProcessCts;
    #endregion

    #region Init Method
    protected override void OnInitialize()
    {
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
        //FadeOn
        //�ش� ������ ��ũ�� �غ�� Clean�� �����Ѵ� .
        //���� ������ FadeOut
    }
    async UniTask StartFrameworkAsync(long index)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(1), ignoreTimeScale: false);
        //�ش� ������ ��ũ�� �������ش� .
        //�ð��� �帣�� �ϰ� .. ���� �͵� ����ŸƮ..
    }
    #endregion
}
