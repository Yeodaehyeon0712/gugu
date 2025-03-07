using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Data
{
    public class SkillData
    {
        public readonly int NameKey;
        public readonly int ExplanationKey;
        public readonly float DurationTime;
        public readonly float CoolTime;
        public readonly int EffectKey;
        public readonly string IconPath;

        IntFormulaCalculator _intCoefficientFormula;
        FloatFormulaCalculator _coefficientFormula;

        long _lastComputeLevel = -1;
        string _lastFormatExplanation;
        eLanguage _currentLanguage;

        public SkillData(Dictionary<string, string> dataPair)
        {
            NameKey = int.Parse(dataPair["NameKey"]);
            ExplanationKey = int.Parse(dataPair["ExplanationKey"]);
            DurationTime = float.Parse(dataPair["DurationTime"]);
            CoolTime = float.Parse(dataPair["CoolTime"]);
            EffectKey = int.Parse(dataPair["EffectKey"]);
            IconPath = dataPair["IconPath"];
            _intCoefficientFormula = new IntFormulaCalculator(dataPair["IntCoeffcient"]);
            _coefficientFormula = new FloatFormulaCalculator(dataPair["Coefficient"]);
        }

        void RecomputeLevel(long level)
        {
            _currentLanguage = RuntimePreference.Data.Language;
            _lastComputeLevel = level;
            _lastFormatExplanation = LocalizingManager.Instance.GetLocalizing(ExplanationKey, _coefficientFormula.GetValue(level).ToString());          
        }
        public string GetExplanationString(long level)
        {
            if (_lastComputeLevel != level)
            {
                RecomputeLevel(level);
            }
            if (_currentLanguage != RuntimePreference.Data.Language)
            {
                _currentLanguage = RuntimePreference.Data.Language;              
                _lastFormatExplanation = LocalizingManager.Instance.GetLocalizing(ExplanationKey, _coefficientFormula.GetValue(level).ToString());
            }
            return _lastFormatExplanation;
        }

        public int GetIntCoefficient(long level) => _intCoefficientFormula.GetValue(level);
        public float GetCoefficient(long level)
        {
            return _coefficientFormula.GetValue(level);
        }


    }
}
public class SkillTable : TableBase
{
    Dictionary<long, BaseSkill> _skillDic = new Dictionary<long, BaseSkill>();
    public Dictionary<long, BaseSkill> GetSkillDic => _skillDic;
    Dictionary<long, string> _indexToAssemblyDic;

    public BaseSkill this[long index]
    {
        get
        {
            if (_skillDic.ContainsKey(index))
                return _skillDic[index];

            return null;
        }
    }

    public string GetAssemlbyClass(long index)
    {
        if (_indexToAssemblyDic.ContainsKey(index))
            return _indexToAssemblyDic[index];

        return "";
    }

    protected override void OnLoad()
    {
        LoadData(_tableName);
        global::System.Reflection.Assembly targetAssembly = global::System.Reflection.Assembly.Load("Assembly-CSharp");
        Dictionary<string, global::System.Type> typeDic = new Dictionary<string, global::System.Type>();
        _indexToAssemblyDic = new Dictionary<long, string>();

        foreach (var contents in _dataDic)
        {
            long index = long.Parse(contents.Value["Index"]);
            string assemblyClass = contents.Value["ClassAssembly"];
            if (!typeDic.ContainsKey(assemblyClass))
                typeDic.Add(assemblyClass, targetAssembly.GetType(assemblyClass));

            Data.SkillData data = new Data.SkillData(contents.Value);
            _indexToAssemblyDic.Add(index, assemblyClass);

            object skill = System.Activator.CreateInstance(typeDic[assemblyClass], index, data);
            _skillDic.Add(index, (BaseSkill)skill);
        }
    }
}
