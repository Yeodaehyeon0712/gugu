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
            equipmentDic.Add(contents.Key, new Data.EquipmentData(contents.Key,contents.Value));
    }
}
