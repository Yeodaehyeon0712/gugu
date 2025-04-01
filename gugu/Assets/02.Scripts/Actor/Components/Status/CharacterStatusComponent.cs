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
            RecomputeStatus(type);
        }
    }
    public override float GetStatus(eStatusType type)=> computedStatusDic[type];
    public override void RecomputeStatus(eStatusType type)
    {
        var statusData = DataManager.StatusTable[type];

        float defaultValue = statusData.DefaultValue;
        float enforceValue = statusData.GetValue(SnapShotDataProperty.Instance.GetStatusLevel(type));
        float equipmentValue = Player.InGameData.GetEquipmentValue(type);

        computedStatusDic[type] = statusData.CalculateType switch
        {
            eCalculateType.Flat => defaultValue + enforceValue + equipmentValue,
            eCalculateType.Percentage => defaultValue * (1 + (0.01f * (enforceValue+equipmentValue))),
            _ => defaultValue
        };
    }
    #endregion
}
