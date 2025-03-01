using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class ItemData
    {
        public long Index;
        public readonly eItemType ItemType;
        public readonly int NameKey;
        public readonly float DurationTime;
        public readonly string ResourcePath;
        public readonly string IconPath;

        public readonly bool UseRandomValue;
        public readonly int MinValue;
        public readonly int MaxValue;

        public ItemData(long index,Dictionary<string,string>dataPair)
        {
            Index = index;
            ItemType = GameConst.ItemType[dataPair["ItemType"]];
            NameKey = int.Parse(dataPair["NameKey"]);
            DurationTime = float.Parse(dataPair["DurationTime"]);
            ResourcePath = dataPair["ResourcePath"];
            IconPath = dataPair["IconPath"];

            string itemValueStr = dataPair["ItemValue"];
            UseRandomValue = itemValueStr.Contains("|");
            if(UseRandomValue)
            {
                string[] range = itemValueStr.Split('|');
                MinValue = int.Parse(range[0]);
                MaxValue = int.Parse(range[1]);
            }
            else
            {
                MinValue = MaxValue=int.Parse(itemValueStr);
            }
        }
        public float GetValue()
        {
            return UseRandomValue ? Random.Range(MinValue, MaxValue + 1) : MinValue;
        }
    }
}
public class ItemTable : TableBase
{
    Dictionary<long, Data.ItemData> itemDic = new Dictionary<long, Data.ItemData>();
    public Dictionary<long, Data.ItemData> GetItemDic => itemDic;
    public Data.ItemData this[long index]
    {
        get
        {
            if (itemDic.ContainsKey(index))
                return itemDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
            itemDic.Add(contents.Key, new Data.ItemData(contents.Key,contents.Value));
    }
}
