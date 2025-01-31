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
    //컴포넌트 초기화 직후 불린다 .
    protected override void OnComponentActive()
    {
        
    }
    protected override void OnComponentInactive()
    {
        Player.ResetSkills();
    }
    //리스트로 변경하는거 고민해봐 .. 
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
