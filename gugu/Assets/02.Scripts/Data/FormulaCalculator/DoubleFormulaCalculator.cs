using System;

public class DoubleFormulaCalculator : FormulaCalculator
{
    double _lastComputeVariable;
    double _prevRefFuncZeroPoint = 0;
    public DoubleFormulaCalculator(string formula) : base(formula)
    {
        Formula = formula;
    }
    void Recompute(long level)
    {
        _lastLevel = level;
        string str = $"{level}.0";
        string val = Formula.Replace("x", str);
        while (_useFuncRef && val.Contains("f"))
            val = ComputeFunctionRef(val);
        while (_usePower && val.Contains("power"))
            val = ComputePower(val);
        _lastComputeVariable = Convert.ToDouble(_dataTable.Compute(val, string.Empty));
    }
    public double GetValue(long level)
    {
        if (_lastLevel != level)
            Recompute(level);

        return _lastComputeVariable;
    }
    protected override string ComputeFunctionRef(string val)
    {
        int start = val.IndexOf("f");
        int end = val.IndexOf(')', start);

        string piece = val.Substring(start + 2, end - (start + 2));
        string[] split = piece.Split('|');

        object compute = _dataTable.Compute(split[0], string.Empty);
        if (split.Length == 2) _prevRefFuncZeroPoint = double.Parse(split[1]);
        int parse = Convert.ToInt32(compute);

        return val.Replace(val.Substring(start, end - start + 1), (parse > 0 ? GetValue(parse) : _prevRefFuncZeroPoint).ToString());
    }
}