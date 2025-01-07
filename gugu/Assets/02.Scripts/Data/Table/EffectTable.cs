using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{

    public class EffectData
    {
        public readonly Dictionary<eEffectChainCondition, int> EffectChainDictionary;
        public readonly Dictionary<eEffectChainCondition, float> EffectChainDurationDictionary;
        public readonly string ResourcePath;
        public readonly int SoundIndex;

        public EffectData(Dictionary<string, string> dataPair)
        {
            if (string.IsNullOrEmpty(dataPair["ChainEffectCondition"]))
            {
                EffectChainDictionary = null;
            }
            else
            {
                string[] keys = dataPair["ChainEffectCondition"].Split('|');
                string[] values = dataPair["ChainEffectKey"].Split('|');
                string[] durations = dataPair["ChainEffectDurationTime"].Split('|');

                EffectChainDictionary = new Dictionary<eEffectChainCondition, int>(keys.Length);
                EffectChainDurationDictionary = new Dictionary<eEffectChainCondition, float>(keys.Length);

                for (int i = 0; i < keys.Length; ++i)
                {
                    EffectChainDictionary.Add(GameConst.EffectChainType[keys[i]], int.Parse(values[i]));
                    EffectChainDurationDictionary.Add(GameConst.EffectChainType[keys[i]], float.Parse(durations[i]));
                }               
            }

            ResourcePath = dataPair["ResourcePath"];
            string soundIndex = dataPair["SoundIndex"];
            SoundIndex = string.IsNullOrEmpty(soundIndex) ? -1 : int.Parse(soundIndex);
        }
    }
}
public class EffectTable : TableBase
{
    Dictionary<long, Data.EffectData> _effectDataDic = new Dictionary<long, Data.EffectData>();
    public Data.EffectData this[long index] => _effectDataDic[index];
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            _effectDataDic.Add(contents.Key, new Data.EffectData(contents.Value));
    }
}
