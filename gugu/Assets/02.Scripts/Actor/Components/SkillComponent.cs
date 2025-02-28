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
    protected override void OnComponentActive()
    {
        Player.SetAvailableSkillList();
        Player.RegisterSkill(DataManager.CharacterTable[owner.ObjectID].DefaultSkillKey);
    }
    protected override void OnComponentInactive()
    {
        Player.ResetSkills();
    }
    protected override void OnComponentUpdate(float deltaTime)
    {
        if (Player.IngameSkillDic.Count < 1) return;

        foreach (var skill in Player.IngameSkillDic.Values)
            skill.UpdateSkill(deltaTime);
    }
    #endregion

    #region Skill Method

    #endregion
}
