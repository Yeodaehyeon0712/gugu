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
        for(eStatusType i=eStatusType.None+1;i<eStatusType.End;++i)
        {
            RecomputeStat(i);
        }
    }

    //1. 디폴트 벨류를 산출한다 .
    //2 . 먼저 스탯의 배율을 곱한다 .
    //3 . 다음은 캐릭터의 배율을 곱한다 . ->근데 여기까지는 초기화 부분에서 가능한거잖아 .
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
        float enforceValue = statusData.GetValue(Player.snapShot.GetStatusLevel(type));
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
