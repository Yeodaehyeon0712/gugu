using System;

public abstract class FormulaCalculator
{
    protected static System.Data.DataTable _dataTable = new System.Data.DataTable();
    public string Formula;
    protected long _lastLevel;
    protected bool _usePower;
    protected bool _useFuncRef;
    public FormulaCalculator(string formula)
    {
        _usePower = formula.Contains("power");
        _useFuncRef = formula.Contains("f");
    }
    protected static string ComputePower(string val)
    {
        int startPower = val.IndexOf("power");
        int endPower = val.IndexOf(')', startPower);

        string powerStringPiece = val.Substring(startPower + 6, endPower - (startPower + 6));
        string[] powerSplitArr = powerStringPiece.Split('^');

        float baseValue = Convert.ToSingle(_dataTable.Compute(powerSplitArr[0], string.Empty));
        float exponent = Convert.ToSingle(_dataTable.Compute(powerSplitArr[1], string.Empty));

        double power = Math.Pow(baseValue, exponent);

        string replaceOriginalString = val.Remove(startPower, endPower - startPower + 1);
        string insertString = replaceOriginalString.Insert(startPower, power.ToString("#0.0e0"));

        return insertString;
    }
    protected virtual string ComputeFunctionRef(string val)
    {
        return string.Empty;
    }
}