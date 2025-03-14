using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillComponent : BaseComponent
{
    #region Fields
    public Dictionary<long, BaseSkill> OwnedSkillDic;
    #endregion

    #region Component Method
    public SkillComponent(Actor owner) : base(owner, eComponent.SkillComponent)
    {
    }
    protected override void OnComponentActive()
    {
        Player.InGameData.SetAvaiableSkillList();
        Player.InGameData.SelectSkill(DataManager.CharacterTable[owner.ObjectID].DefaultSkillKey);
        OwnedSkillDic = Player.InGameData.Data.OwnedSkillDic;
    }
    protected override void OnComponentInactive()
    {
        Player.InGameData.ResetSkills();
    }
    protected override void OnComponentUpdate(float deltaTime)
    {
        if (OwnedSkillDic.Count < 1) return;

        foreach (var skill in OwnedSkillDic.Values)
            skill.UpdateSkill(deltaTime);
    }
    #endregion


}
