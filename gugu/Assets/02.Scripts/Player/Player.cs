using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    #region Fields
    public static Actor PlayerCharacter;
    //���� �ϳ��� ������ Ȥ�� Ŭ������ ���� �͵�
    public static Dictionary<long,BaseSkill> IngameSkillDic = new Dictionary<long, BaseSkill>();
    #endregion

    public static void Initialize()
    {
        //���⼭ �⺻ ������ �ִ� �����͸� �޴´� .
    }

    #region Skill
    public static void RegisterSkill(long index)
    {
        if (IngameSkillDic.ContainsKey(index) || PlayerCharacter == null) return;

        var skill = DataManager.SkillTable[index];
        IngameSkillDic.Add(index,skill.RegisterSkill(PlayerCharacter));
    }
    public static void LevelUpSkill(long index)
    {
        if (IngameSkillDic.TryGetValue(index, out var skill) == false || skill.isMaxLevel) return;       
        skill.LevelUpSkill();      
    }
    public static void ResetSkills()
    {
        foreach (var item in IngameSkillDic)
        {
            item.Value.UnregisterSkill();
            IngameSkillDic.Remove(item.Key);
        }
    }
    #endregion
}
