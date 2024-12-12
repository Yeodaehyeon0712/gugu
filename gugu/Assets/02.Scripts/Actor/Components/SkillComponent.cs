using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillComponent : BaseComponent
{
    //���Ⱑ �ƴ϶� �÷��̾�� �����ұ� ..
    List<BaseSkill> attackSkillList = new List<BaseSkill>();
    public SkillComponent(Actor owner) : base(owner, eComponent.SkillComponent)
    {

    }
    protected override void OnComponentUpdate(float fixedDeltaTime)
    {
        foreach(var skill in attackSkillList)
        {
            skill.UpdateSkill(fixedDeltaTime);
        }
    }
    public void GetSkill(long index)
    {
        var skill = DataManager.SkillTable[index];
        skill.RegisterSkill(owner);
        attackSkillList.Add(skill);
    }
    public void LevelUpSkill(long index)
    {
        //�̰� ����ع� ..
        attackSkillList[0].LevelUpSkill(false);
    }
}
