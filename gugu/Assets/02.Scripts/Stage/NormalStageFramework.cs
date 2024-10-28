using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalStageFramework : StageFramework
{
    public override async UniTask StartStageAsync(long stageIndex)
    {
        await UniTask.WaitForFixedUpdate();
        while(true)//������ ���������� ..
        {
            //2�ʸ� ī�����Ѵ� . 
            await UniTask.Delay(2000);
            Debug.Log("2�� ���");
            //2�ʰ� ����ߴٸ� ���͸� ����. ������ �Ϸ�ɶ����� ����Ѵ� .
            //������ �Ϸ�ȴٸ� ���� ī������ �ٽ� ����. 
            //�ش� ������ �÷��̾ �װų� Ȥ�� Ÿ�̸Ӱ� �� �� ��쿡�� ����� . 
            //30��/2�и��� ���Ͱ� ���� ������ . .. 
            await SpawnGroupAsync();
        }


        //�Ϲ� ��ȯ / ���� ��ȯ ������ ���� �ٸ� �� ���� >?
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
        Debug.Log("��� ��ȯ");
    }
}
