using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatContainer 
{
    Dictionary<long, SOEnemyStat> enemyStatDic = new Dictionary<long, SOEnemyStat>();

    public EnemyStatContainer(Dictionary<long, Data.EnemyData> enemyDataDic)
    {
        CreateEnemyStat(enemyDataDic);
    }

    public SOEnemyStat this[long index]
    {
        get
        {
            if (enemyStatDic.ContainsKey(index))
                return enemyStatDic[index];
            return null;
        }
    }

    void CreateEnemyStat(Dictionary<long, Data.EnemyData> enemyDataDic)
    {
        foreach (var contents in enemyDataDic)
        {
            var stat = ScriptableObject.CreateInstance<SOEnemyStat>();
            stat.HP = contents.Value.HP;
            enemyStatDic.Add(contents.Key, stat);
        }
    }
}
