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

        float totalEnfoce = enforceValue + equipmentValue;
        float modifier = (statusData.ModifierType == eModifierType.Increase) ? 1f : -1f;

        computedStatusDic[type] = statusData.CalculateType switch
        {
            eCalculateType.Flat => (defaultValue + totalEnfoce) * modifier,
            eCalculateType.Percentage => defaultValue * (1f + (totalEnfoce*0.01f*modifier)),
            _ =>defaultValue,
        };
    }
    #endregion
}
