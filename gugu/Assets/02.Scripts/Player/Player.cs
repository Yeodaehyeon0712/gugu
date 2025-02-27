using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    #region Fields
    public static Actor PlayerCharacter;
    //���� �ϳ��� ������ Ȥ�� Ŭ������ ���� �͵�
    public static Dictionary<long,BaseSkill> IngameSkillDic = new Dictionary<long, BaseSkill>();
    static List<long> availableSkillList;
    static HashSet<long> selectedSkillSet;
    #endregion

    #region Init Method
    public static void Initialize()
    {
        //���⼭ �⺻ ������ �ִ� �����͸� �޴´� .
        
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
        UIManager.Instance.FieldUI.FindFieldUI<FieldUI_HPBar>(eFieldUI.HPBar).Disable();//�̰͵� ����


        PlayerCharacter.Death(0f);
        PlayerCharacter = null;
    }
    #endregion

    #region Skill
    public static void SetAvailableSkillList()
    {
        foreach (var skill in DataManager.SkillTable.GetSkillDic.Values)
        {
            //���� ��ų�� ����ִٸ� ���ϱ� �ʴ´� 
            availableSkillList.Add(skill.Index);
        }
    }
    public static HashSet<long> GetRandomSkillSet(int count)
    {
        selectedSkillSet.Clear();
        int targetSkillCount = Mathf.Min(count, availableSkillList.Count);

        while (selectedSkillSet.Count < targetSkillCount)
        {
            int randomIndex = Random.Range(0, availableSkillList.Count);
            selectedSkillSet.Add(availableSkillList[randomIndex]);
        }
        return selectedSkillSet;
    }
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

        if (skill.isMaxLevel&&availableSkillList.Contains(skill.Index))
            availableSkillList.Remove(skill.Index);
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

        availableSkillList.Clear();
        selectedSkillSet.Clear();
    }
    #endregion
}
