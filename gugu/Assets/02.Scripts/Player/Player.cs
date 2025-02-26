using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    #region Fields
    public static Actor PlayerCharacter;
    //추후 하나의 데이터 혹은 클래스로 묶을 것들
    public static Dictionary<long,BaseSkill> IngameSkillDic = new Dictionary<long, BaseSkill>();
    #endregion

    public static void Initialize()
    {
        //여기서 기본 가지고 있는 데이터를 받는다 .
        
    }
    public static void RegisterPlayer(Actor actor)
    {
        PlayerCharacter = actor;
        //Camera
        CameraManager.Instance.RegisterFollowTarget(PlayerCharacter.transform);
        //Spawn Area
        ActorManager.Instance.RegisterSpawnAreaParent(PlayerCharacter.transform);

        UIManager.Instance.FieldUI.SetHPBar(PlayerCharacter);
    }
    public static void UnRegisterPlayer()
    {
        if (PlayerCharacter == null) return;
        //Camera
        CameraManager.Instance.RegisterFollowTarget(null);
        //SpawnArea
        ActorManager.Instance.RegisterSpawnAreaParent(null);

        //UI
        UIManager.Instance.FieldUI.FindFieldUI<FieldUI_HPBar>(eFieldUI.HPBar).Disable();//이것도 수정

        PlayerCharacter.Death(0f);
        PlayerCharacter = null;
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
        var keysToRemove = new List<long>(); 
        foreach (var item in IngameSkillDic)
        {
            item.Value.UnregisterSkill();
            keysToRemove.Add(item.Key); 
        }

        foreach (var key in keysToRemove)
        {
            IngameSkillDic.Remove(key);
        }
    }
    #endregion
}
