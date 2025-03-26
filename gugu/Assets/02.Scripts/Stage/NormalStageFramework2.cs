using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NormalStageFramework2 : StageFramework
{
    #region Fields
    int subWaveCount = 1;
    float elapsedTime = 0;
    Timer timer;
    #endregion

    #region Process Method
    protected override async UniTask ProcessFrameworkAsync(long stageIndex, CancellationToken token)
    {
        timer = new Timer(0, true, onUpdate:(time) => UIManager.Instance.BattleStateUI.SetTimerText(time));

        foreach (var waveData in DataManager.StageTable[stageIndex].WaveDataArr)
        {
            if (frameworkState != eStageFrameworkState.InProgress) break;

            await MainWavePorccess(waveData,timer,token);
            //await BossPorccess(token);
        }
        frameworkState = eStageFrameworkState.Victory;
    }
    
    async UniTask MainWavePorccess(Data.WaveData waveData,Timer timer,CancellationToken token)
    {
        for (int i=0;i<waveData.SubWaveArr.Length;i++)
        {
            if (frameworkState != eStageFrameworkState.InProgress) break;

            var subWave = waveData.SubWaveArr[i];
            SetSubWaveTimer(timer);
            await SubWaveProcess(subWave,timer,token);
        }     
    }
    async UniTask SubWaveProcess(Data.SubWave subWave,Timer subWaveTimer, CancellationToken token)
    {
        subWaveCount++;

        while (frameworkState == eStageFrameworkState.InProgress && subWaveTimer.IsOverTime == false)
        {
            elapsedTime += TimeManager.DeltaTime;

            if (elapsedTime > GameConst.spawnInterval)
            {
                elapsedTime = 0;
                await SpawnGroupAsync(subWave.MonsterIndexArr, subWave.MonsterCount, token);
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
    void SetSubWaveTimer(Timer subWaveTimer)
    {
        float subWaveTime = GameConst.subWaveTime;
        float startTime = subWaveTime * (subWaveCount - 1);
        float targetTime = subWaveTime * subWaveCount;
        subWaveTimer.StartTimer(targetTime, startTime);      
    }
    #endregion

    #region Framework Method
    protected override void OnCleanFramework()
    {
        TimeManager.Instance.RemoveTimer = timer;
        timer = null;
        subWaveCount = 1;
        elapsedTime = 0;

        base.OnCleanFramework();
    }
    #endregion
}
