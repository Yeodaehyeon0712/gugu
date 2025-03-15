using System.Collections.Generic;
namespace Data
{
    public class EquipmentData
    {
        public long Index;
        public readonly eStatusType TargetStatus;
        public readonly eCalculateType CalculateType;
        readonly FloatFormulaCalculator valueFormula;
        public readonly string IconPath;
        public readonly int NameKey;
        public readonly int ExplanationKey;
        public EquipmentData(long index,Dictionary<string, string> dataPair)
        {
            Index = index;
            TargetStatus = GameConst.StatusType[dataPair["TargetStatus"]];
            CalculateType = GameConst.CalculateType[dataPair["CalculateType"]];
            valueFormula = new FloatFormulaCalculator(dataPair["ValueCalculator"]);
            IconPath = dataPair["IconPath"];
            NameKey = int.Parse(dataPair["NameKey"]);
            ExplanationKey = int.Parse(dataPair["ExplanationKey"]);
        }
        public float GetValue(long level) => valueFormula.GetValue(level);
    }
}
public class EquipmentTable : TableBase
{
    Dictionary<long, Data.EquipmentData> equipmentDic = new Dictionary<long, Data.EquipmentData>();
    public Dictionary<long, Data.EquipmentData> GetEquipmentDic => equipmentDic;
    Dictionary<eStatusType, long> equipmentStatusDic=new Dictionary<eStatusType, long>();
    public Data.EquipmentData this[long index]
    {
        get
        {
            if (equipmentDic.ContainsKey(index))
                return equipmentDic[index];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);
        foreach (var contents in _dataDic)
        {
            var data = new Data.EquipmentData(contents.Key, contents.Value);
            equipmentDic.Add(contents.Key, data);
            equipmentStatusDic.Add(data.TargetStatus, data.Index);
        }
    }
    public long GetEquipmentByStatus(eStatusType type)
    {
        return equipmentStatusDic.TryGetValue(type, out long index) ? index : 0;
    }
}
