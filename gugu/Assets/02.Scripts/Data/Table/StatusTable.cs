using System.Collections.Generic;
namespace Data
{
    public class StatusData
    {
        public readonly eStatusType StatusType;
        public readonly float DefaultValue;
        public readonly eCalculateType CalculateType;
        readonly FloatFormulaCalculator valueFormula;
        readonly IntFormulaCalculator goldFormula;
        public readonly string IconPath;
        public readonly int NameKey;
        public readonly int ExplanationKey;
        public StatusData(Dictionary<string, string> dataPair)
        {
            StatusType = GameConst.StatusType[dataPair["Name"]];
            DefaultValue = float.Parse(dataPair["DefaultValue"]);
            CalculateType = GameConst.CalculateType[dataPair["CalculateType"]];
            valueFormula = new FloatFormulaCalculator(dataPair["ValueCalculator"]);
            goldFormula = new IntFormulaCalculator(dataPair["GoldCalculator"]);
            IconPath = dataPair["IconPath"];
            NameKey = int.Parse(dataPair["NameKey"]);
            ExplanationKey = int.Parse(dataPair["ExplanationKey"]);
        }
        public float GetValue(long level)=> valueFormula.GetValue(level);
        public int GetGold(long level) => goldFormula.GetValue(level);
    }
}
public class StatusTable : TableBase
{
    Dictionary<eStatusType, Data.StatusData> statusDic = new Dictionary<eStatusType, Data.StatusData>();
    public Dictionary<eStatusType, Data.StatusData> GetStatusDic => statusDic;
    public Data.StatusData this[eStatusType type]
    {
        get
        {
            if (statusDic.ContainsKey(type))
                return statusDic[type];
            return null;
        }
    }
    protected override void OnLoad()
    {
        LoadData(_tableName);

        foreach (var contents in _dataDic)
        {
            Data.StatusData data = new Data.StatusData(contents.Value);
            statusDic.Add(data.StatusType, data);
        }
    }
}
