using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatusComponent : StatusComponent
{
    #region Fields
    Dictionary<eStatusType, float> computedStatusDic = new Dictionary<eStatusType, float>();
    #endregion

    #region Status Method
    public CharacterStatusComponent(Actor owner) : base(owner)
    {

    }
    public override void SetDefaultStatus()
    {
        foreach (eStatusType type in System.Enum.GetValues(typeof(eStatusType)))
        {
            RecomputeStat(type);
        }
    }
    public void LevelUpItem(eStatusType type)
    {
        //if (itemLevelDic.TryGetValue(type, out var level))
        //{
        //    itemLevelDic[type] = (long)Mathf.Min(level + 1, GameConst.MaxStatusLevel);
        //}
        //else
        //    itemLevelDic.Add(type, 1);

        RecomputeStat(type);
    }

    public override float GetStatus(eStatusType type)
    {
        if (computedStatusDic.TryGetValue(type, out var value))
            return value;
        else
            return DataManager.StatusTable[type].DefaultValue;
    }
    public void RecomputeStat(eStatusType type)
    {
        var statusData = DataManager.StatusTable[type];

        float defaultValue = statusData.DefaultValue;
        float enforceValue = statusData.GetValue(Player.SnapShot.GetStatusLevel(type));
        float equipmentValue = 1;

        computedStatusDic[type] = statusData.CalculateType switch
        {
            eCalculateType.Flat => defaultValue + enforceValue + equipmentValue,
            eCalculateType.Percentage => defaultValue * (1 + (0.01f * enforceValue)) * equipmentValue,
            _ => defaultValue
        };
    }
    #endregion
}
