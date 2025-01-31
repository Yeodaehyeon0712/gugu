using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillComponent : BaseComponent
{
    #region Fields

    #endregion

    #region Component Method
    public SkillComponent(Actor owner) : base(owner, eComponent.SkillComponent)
    {

    }
    //������Ʈ �ʱ�ȭ ���� �Ҹ��� .
    protected override void OnComponentActive()
    {
        
    }
    protected override void OnComponentInactive()
    {
        Player.ResetSkills();
    }
    //����Ʈ�� �����ϴ°� ����غ� .. 
    protected override void OnComponentUpdate(float fixedDeltaTime)
    {
        if (Player.IngameSkillDic.Count < 1) return;

        foreach (var skill in Player.IngameSkillDic.Values)
            skill.UpdateSkill(fixedDeltaTime);
    }
    #endregion

    #region Skill Method

    #endregion
}
