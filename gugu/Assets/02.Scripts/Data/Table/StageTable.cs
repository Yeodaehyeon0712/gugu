using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class StageData
    {
        public readonly long Index;
        public readonly long NameKey;
        public readonly string BackgroundPath;
        public readonly WaveData[] WaveDataArr;


        public StageData(long index, Dictionary<string, string> dataPair)
        {
            Index = index;
            NameKey = long.Parse(dataPair["NameKey"]);
            BackgroundPath = dataPair["BackgroundPath"];

            var WaveIndexArr = System.Array.ConvertAll(dataPair[$"WaveIndex"].Split('|'), v => int.Parse(v));
            WaveDataArr = new WaveData[WaveIndexArr.Length];
            for (int i=0;i<WaveIndexArr.Length;i++)
            {
                WaveDataArr[i] = DataManager.WaveTable[WaveIndexArr[i]];
            }
        }
    }

}
public class StageTable : TableBase
{
    Dictionary<long, Data.StageData> stageDataDic = new Dictionary<long, Data.StageData>();
    public Dictionary<long, Data.StageData> GetDataDic => stageDataDic;
    public Data.StageData this[long index]
    {
        get
        {
            if (stageDataDic.ContainsKey(index))
                return stageDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            stageDataDic.Add(contents.Key, new Data.StageData(contents.Key, contents.Value));
    }
}
