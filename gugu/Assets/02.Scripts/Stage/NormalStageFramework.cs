using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    #region Fields
    int mainWaveCount;
    int subWaveCount;
    #endregion


    #region Framework Method
    protected override async UniTask ProcessFrameworkAsync(long stageIndex, CancellationToken token)
    {
        frameworkState = eStageFrameworkState.InProgress;
        mainWaveCount = 0;

        var mainWaveTimer = new Timer(0,true);
        var subWaveTimer = new Timer(0, true);
        var spawnTimer = new Timer(0, true);

        try
        {
            foreach (var waveData in DataManager.StageTable[stageIndex].WaveDataArr)
            {
                if (frameworkState != eStageFrameworkState.InProgress) break;

                ++mainWaveCount;
                await MainWavePorccess(waveData,mainWaveTimer,subWaveTimer,spawnTimer,token);
                await BossPorccess(token);
            }

            await UniTask.Yield(PlayerLoopTiming.Update, token);

            switch (frameworkState)
            {
                case eStageFrameworkState.Victory:
                    break;
                case eStageFrameworkState.Defeat:
                    break;
            }
            ShowVicotry();       
        }
        catch (System.OperationCanceledException)
        {
            //�̰� ���ݴ� ����غ��� ..
            Debug.Log("NormalFrameworkProcess was canceled.");
            TimeManager.Instance.RemoveTimer = mainWaveTimer;
            TimeManager.Instance.RemoveTimer = subWaveTimer;
            TimeManager.Instance.RemoveTimer = spawnTimer;
        }
    }
    #endregion
    //�ϳ��� ���� ���̺�� / 10���� ���� ���̺� / �ϳ��� ���̺�� 30�ʷ� ������
    #region Normal Stage Process
    async UniTask MainWavePorccess(Data.WaveData waveData,Timer mainWaveTimer,Timer subWaveTimer,Timer spawnTimer, CancellationToken token)
    {
        float mainWaveTime = GameConst.subWaveTime * waveData.SubWaveArr.Length;
        float startTime = mainWaveTime * (mainWaveCount - 1);
        float targetTime = mainWaveTime * mainWaveCount;

        mainWaveTimer.StartTimer(targetTime, startTime);   

        var subWaveTask = SubWaveProcess(waveData.SubWaveArr.Length,subWaveTimer,token);
        var spawnEnemyTask = SpawnEnemyProcess(waveData.SubWaveArr,spawnTimer,token);

        while (frameworkState == eStageFrameworkState.InProgress && mainWaveTimer.IsOverTime==false)
        {
            CheckStageState();
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }    
    }

    async UniTask SubWaveProcess(int waveCount, Timer timer, CancellationToken token)
    {
        subWaveCount = 0;
        float subWaveTime = GameConst.subWaveTime;
        while (frameworkState == eStageFrameworkState.InProgress&&subWaveCount< waveCount)
        {
            if(timer.IsOverTime)
            {
                subWaveCount++;
                timer.StartTimer(subWaveTime);
            }
            await UniTask.Yield(PlayerLoopTiming.Update,token);
        }
    }
    async UniTask SpawnEnemyProcess(Data.SubWave[] subWaveArr,Timer timer, CancellationToken token)//10�� �ݺ�
    {
        float spawnTime = GameConst.spawnInterval;
        while (frameworkState == eStageFrameworkState.InProgress)
        {
            if (timer.IsOverTime)
            {
                var subWave = subWaveArr[subWaveCount];
                await SpawnGroupAsync(subWave.MonsterIndexArr, subWave.MonsterCount,token);
                timer.StartTimer(spawnTime);
            }
            await UniTask.Yield(PlayerLoopTiming.Update, token);
        }
    }
    async UniTask SpawnGroupAsync(int[] monsterIndexArr, int spawnCount, CancellationToken token)
    {
        var spawnTasks = new List<UniTask>();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = ActorManager.Instance.GetRandomPosition();
            int enemyIndex = monsterIndexArr[Random.Range(0, monsterIndexArr.Length)];

            spawnTasks.Add(ActorManager.Instance.SpawnEnemy(enemyIndex, spawnPosition));
        }
        await UniTask.WhenAll(spawnTasks);
    }

    async UniTask BossPorccess(CancellationToken token)
    {
        await UniTask.Yield(PlayerLoopTiming.Update, token);
    }
    #endregion

    #region Result Method
    void CheckStageState()
    {
        //�÷��̾��� ������ üũ�Ѵ� .
        if (Player.PlayerCharacter.ActorState == eActorState.Death)
        {
            frameworkState = eStageFrameworkState.Defeat;
            //StopTimer();
            //break;
        }
    }
    public void ShowVicotry()
    {
        TimeManager.Instance.IsActiveTimeFlow = false;
        Debug.Log("tm");
    }

    protected override void OnCleanFramework()
    {
        throw new System.NotImplementedException();
    }
    #endregion
}
