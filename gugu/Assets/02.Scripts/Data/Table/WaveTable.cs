using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class WaveData
    {
        public readonly long Index;
        public readonly int BossIndex;
        public readonly SubWave[] SubWaveArr=new SubWave[10];

        public WaveData(long index, Dictionary<string, string> dataPair)
        {
            Index = index;

            for(int i=0;i< 10; i++)
            {
                var monsterCount = int.Parse(dataPair[$"MonsterCount_{i+1}"]);
                var MonsterIndexArr = System.Array.ConvertAll(dataPair[$"MonsterIndex_{i+1}"].Split('|'), v => int.Parse(v));
                SubWaveArr[i] = new SubWave(monsterCount, MonsterIndexArr);
            }
        }
    }
    public readonly struct SubWave
    {
        public readonly int MonsterCount;
        public readonly int[] MonsterIndexArr;
        public SubWave(int monsterCount, int[] monsterIndexArr)
        {
            MonsterCount = monsterCount;
            MonsterIndexArr = monsterIndexArr;
        }
    }
}
public class WaveTable : TableBase
{
    Dictionary<long, Data.WaveData> waveDataDic = new Dictionary<long, Data.WaveData>();
    public Dictionary<long, Data.WaveData> GetDataDic => waveDataDic;
    public Data.WaveData this[long index]
    {
        get
        {
            if (waveDataDic.ContainsKey(index))
                return waveDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            waveDataDic.Add(contents.Key, new Data.WaveData(contents.Key, contents.Value));
    }
}
