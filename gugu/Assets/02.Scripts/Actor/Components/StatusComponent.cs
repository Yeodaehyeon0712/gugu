using System.Collections.Generic;
using UnityEngine;

//이건 캐릭터 스탯 .. 
[System.Serializable]
public class StatusComponent : BaseComponent
{
    #region Fields
    //일단 여기 두고 위치는 변경 할 수도 있을것 같음 .
    Dictionary<eStatusType, float> computedStatusDic=new Dictionary<eStatusType, float>();

    #endregion

    #region Component Method
    public StatusComponent(Actor owner) : base(owner, eComponent.StatComponent,useUpdate:false)
    {
        //스탯과 스탯 데이터로 구성 ..
        SetDefaultStat();
    }

    #endregion

    #region Stat Method
    public void SetDefaultStat()
    {
        for(eStatusType i=eStatusType.None+1;i<eStatusType.End;++i)
        {
            RecomputeStat(i);
        }
        //1. 디폴트 벨류를 산출한다 .
        //2 . 먼저 스탯의 배율을 곱한다 .
        //3 . 다음은 캐릭터의 배율을 곱한다 . ->근데 여기까지는 초기화 부분에서 가능한거잖아 .
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
    public float GetStat(eStatusType type)
    {
        if (computedStatusDic.TryGetValue(type, out var value))
            return value;
        else
            return DataManager.StatusTable[type].DefaultValue;
    }
    public void RecomputeStat(eStatusType type)
    {
        //이건 플레이어에서 땡겨올수도 .. 일단 이렇게 하자 .
        var result = DataManager.StatusTable[type].DefaultValue;
        // 1. InGame Status
        //result *= (float)DataManager.StatusTable[type].GetValue(itemLevelDic[type]);
        // 2 . 기타 등등 .. 의 값을 거쳐서 최종값이 산출 될 것이다 .
        computedStatusDic[type] = result;

        //1 . 강화 스탯
        //2 . 캐릭터 기본 스탯
        //3 . 인게임 아이템 스탯 =>이렇게 3가지가 계산되어 산출되는게 최종 값 . 


        //1. 디폴트 벨류를 산출한다 .
        //2 . 먼저 스탯의 배율을 곱한다 .
        //3 . 다음은 캐릭터의 배율을 곱한다 . ->근데 여기까지는 초기화 부분에서 가능한거잖아 .
        //4 . 이후 아이템 레벨업마다 위 값에다 곱해준다 .
    }
    #endregion
}
