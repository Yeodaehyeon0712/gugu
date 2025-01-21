using System.Collections.Generic;
using UnityEngine;

//�̰� ĳ���� ���� .. 
[System.Serializable]
public class StatusComponent : BaseComponent
{
    #region Fields
    //�ϴ� ���� �ΰ� ��ġ�� ���� �� ���� ������ ���� .
    Dictionary<eStatusType, float> computedStatusDic=new Dictionary<eStatusType, float>();

    #endregion

    #region Component Method
    public StatusComponent(Actor owner) : base(owner, eComponent.StatComponent,useUpdate:false)
    {
        //���Ȱ� ���� �����ͷ� ���� ..
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
        //1. ����Ʈ ������ �����Ѵ� .
        //2 . ���� ������ ������ ���Ѵ� .
        //3 . ������ ĳ������ ������ ���Ѵ� . ->�ٵ� ��������� �ʱ�ȭ �κп��� �����Ѱ��ݾ� .
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
        //�̰� �÷��̾�� ���ܿü��� .. �ϴ� �̷��� ���� .
        var result = DataManager.StatusTable[type].DefaultValue;
        // 1. InGame Status
        //result *= (float)DataManager.StatusTable[type].GetValue(itemLevelDic[type]);
        // 2 . ��Ÿ ��� .. �� ���� ���ļ� �������� ���� �� ���̴� .
        computedStatusDic[type] = result;

        //1 . ��ȭ ����
        //2 . ĳ���� �⺻ ����
        //3 . �ΰ��� ������ ���� =>�̷��� 3������ ���Ǿ� ����Ǵ°� ���� �� . 


        //1. ����Ʈ ������ �����Ѵ� .
        //2 . ���� ������ ������ ���Ѵ� .
        //3 . ������ ĳ������ ������ ���Ѵ� . ->�ٵ� ��������� �ʱ�ȭ �κп��� �����Ѱ��ݾ� .
        //4 . ���� ������ ���������� �� ������ �����ش� .
    }
    #endregion
}
