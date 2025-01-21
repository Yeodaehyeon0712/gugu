using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class EnemyData
    {
        public long Index;
        public int NameKey;       
        public string ResourcePath;
        public string AnimatorPath;
        public int PathHash;
        public bool IsRanged;
        public bool IsBoss;
        public SOEnemyStatus EnemyStatus;
        public EnemyData(long index, Dictionary<string, string> dataPair)
        {
            Index = index;
            NameKey = int.Parse(dataPair["NameKey"]);           
            AnimatorPath = dataPair["AnimatorPath"];
            ResourcePath = dataPair["ResourcePath"];
            PathHash = ResourcePath.GetHashCode();
            IsRanged = bool.Parse(dataPair["IsRanged"]);
            IsBoss = bool.Parse(dataPair["IsBoss"]);
            EnemyStatus = SOEnemyStatus.CreateEnemyStatus(dataPair);
        }
    }
}
public class EnemyTable : TableBase
{
    Dictionary<long, Data.EnemyData> enemyDataDic = new Dictionary<long, Data.EnemyData>();
    public Dictionary<long, Data.EnemyData> GetDataDic => enemyDataDic;
    public Data.EnemyData this[long index]
    {
        get
        {
            if (enemyDataDic.ContainsKey(index))
                return enemyDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            enemyDataDic.Add(contents.Key, new Data.EnemyData(contents.Key, contents.Value));
    }   
}
