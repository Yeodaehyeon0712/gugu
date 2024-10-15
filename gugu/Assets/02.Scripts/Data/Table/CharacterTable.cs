using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class CharacterData
    {
        public long Index;
        public int NameKey;
        public float HP;
        public float MoveSpeed;
        public int DefaultSkillKey;
        public string AnimatorPath;
        public int PathHash;
        public string IconPath;

        public CharacterData(long index,Dictionary<string,string>dataPair)
        {
            Index = index;
            NameKey= int.Parse(dataPair["NameKey"]);
            HP = float.Parse(dataPair["HP"]);
            MoveSpeed = float.Parse(dataPair["MoveSpeed"]);
            DefaultSkillKey = int.Parse(dataPair["DefaultSkillKey"]);
            AnimatorPath=dataPair["AnimatorPath"];
            PathHash = AnimatorPath.GetHashCode();
            IconPath = dataPair["IconPath"];
        }
    }
}
public class CharacterTable : TableBase
{
    Dictionary<long, Data.CharacterData>  characterDataDic = new Dictionary<long, Data.CharacterData>();
    public Dictionary<long, Data.CharacterData> GetDataDic => characterDataDic;
    public Data.CharacterData this[long index]
    {
        get
        {
            if (characterDataDic.ContainsKey(index))
                return characterDataDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            characterDataDic.Add(contents.Key, new Data.CharacterData(contents.Key, contents.Value));
    }

    
}
