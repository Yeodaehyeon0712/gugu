using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    public override async UniTask StartStageAsync(long stageIndex)
    {
        await UniTask.WaitForFixedUpdate();
        while(true)//게임이 끝날때까지 ..
        {
            //2초를 카운팅한다 . 
            await UniTask.Delay(2000);
            Debug.Log("2초 경과");
            //2초가 경과했다면 몬스터를 스폰. 스폰이 완료될때까지 대기한다 .
            //스폰이 완료된다면 이제 카운팅을 다시 시작. 
            //해당 과정은 플레이어가 죽거나 혹은 타이머가 다 된 경우에는 멈춘다 . 
            //30초/2분마다 몬스터가 많이 스폰됨 . .. 
            await SpawnGroupAsync();
        }


        //일반 소환 / 보스 소환 유형에 따라 다른 걸 진행 >?
    }
    async UniTask SpawnGroupAsync()
    {
        var spawnTasks = new List<UniTask>();

        for(int i=0;i<5;i++)
        {
            var a= SpawnManager.Instance.GetRandomPosition();
            spawnTasks.Add(SpawnManager.Instance.SpawnEnemy<Enemy>(1,a));
        }
        // Add each spawn task to the list
        // Wait for all spawn tasks to complete
        await UniTask.WhenAll(spawnTasks);
        Debug.Log("모두 소환");
    }
}
