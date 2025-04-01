using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameData
{
    #region Fields
    //Level
    public int CurrentLevel;
    public float CurrentExp;
    public int NextLevelExp;

    //Record
    public int KillCount;
    public int GoldCount;

    //Skill
    public List<long> AvailableSkillList;
    public HashSet<long> SelectedSkillSet;
    public Dictionary<long, BaseSkill> OwnedSkillDic;

    //Equipment
    public List<long> AvailableEquipmentList;
    public HashSet<long> SelectedEquipmentSet;
    public Dictionary<long, long> OwnedEquipmentDic;
    #endregion

    public IngameData()
    {
        //Level
        CurrentLevel = 1;
        CurrentExp = 0;
        NextLevelExp = 0;

        //Record
        KillCount = 0;
        GoldCount = 0;

        //Skill
        AvailableSkillList = new List<long>();
        SelectedSkillSet = new HashSet<long>();
        OwnedSkillDic = new Dictionary<long, BaseSkill>();

        //Equipment
        AvailableEquipmentList = new List<long>();
        SelectedEquipmentSet = new HashSet<long>();
        OwnedEquipmentDic = new Dictionary<long, long>();//Composed with Index,Level
    }
    public void CleanData()
    {
        //Level
        CurrentLevel = 1;
        CurrentExp = 0;
        NextLevelExp = 0;

        //Record
        KillCount = 0;
        GoldCount = 0;

        //Skill
        AvailableSkillList.Clear();
        SelectedSkillSet.Clear();
        OwnedSkillDic.Clear();

        //Equipment
        AvailableEquipmentList.Clear();
        SelectedEquipmentSet.Clear();
        OwnedEquipmentDic.Clear();
    }
}
public class IngameDataProperty 
{
    #region Fields
    public IngameData Data => data;
    IngameData data;
    public float CurrentExp
    {
        get => data.CurrentExp;
        set
        {
            data.CurrentExp = value;
            var per = Mathf.Clamp01(data.CurrentExp / data.NextLevelExp);
            battleStateUI.SetLevelPercentage(per);
        }
    }
    public int CurrentLevel
    {
        get => data.CurrentLevel;
        set
        {
            data.CurrentLevel = value;
            battleStateUI.SetLevel(data.CurrentLevel);
        }
    }
    BattleStateUI battleStateUI;
    Character owner;
    #endregion

    #region Init Method
    public IngameDataProperty()
    {
        data=new IngameData();
    }
    public void InitializeData()
    {
        battleStateUI = UIManager.Instance.BattleStateUI;
        owner = Player.PlayerCharacter;
        InitSkillData();
        InitEquipmentData();
        InitLevelData();
    }
    public void CleanData()
    {
        ResetSkills();
        data.CleanData();
        battleStateUI = null;
        owner = null;
    }
    #endregion

    #region Skill Method
    void InitSkillData()
    {
        //SetAvaiableSkillList
        var skillDic = DataManager.SkillTable.GetSkillDic;

        foreach (var skill in skillDic.Values)
            data.AvailableSkillList.Add(skill.Index);
    }
    int GetAvailableSkillCount()
    {
        var availableSlotCount = 6;
        foreach (var skill in data.OwnedSkillDic)
        {
            if (skill.Value.isMaxLevel)
                availableSlotCount--;
        }
        return Mathf.Min(availableSlotCount, data.AvailableSkillList.Count);
    }
    public HashSet<long> GetRandomSkillSet(int count)
    {
        data.SelectedSkillSet.Clear();

        while (data.SelectedSkillSet.Count < count)
        {
            int randomIndex = Random.Range(0, data.AvailableSkillList.Count);
            data.SelectedSkillSet.Add(data.AvailableSkillList[randomIndex]);
        }
        return data.SelectedSkillSet;
    }
    public void SelectSkill(long index)
    {
        if (data.OwnedSkillDic.ContainsKey(index) == false)
            RegisterSkill(index);
        else
            LevelUpSkill(index);
    }
    void RegisterSkill(long index)
    {
        var skill = DataManager.SkillTable[index];
        data.OwnedSkillDic.Add(index, skill.RegisterSkill(owner));

        if (data.OwnedSkillDic.Count == 6)
        {
            data.AvailableSkillList.RemoveAll(skill => data.OwnedSkillDic.ContainsKey(skill)==false);
        }
    }
    void LevelUpSkill(long index)
    {
        if (data.OwnedSkillDic.TryGetValue(index, out var skill) == false) return;

        skill.LevelUpSkill();

        if (skill.isMaxLevel)
            data.AvailableSkillList.Remove(skill.Index);
    }
    void ResetSkills()
    {
        foreach (var item in data.OwnedSkillDic)
        {
            item.Value.UnregisterSkill();
        }
    }
    #endregion

    #region Equipment Method
    void InitEquipmentData()
    {
        //SetAvaiableEquipmentList()
        var equipmentDic = DataManager.EquipmentTable.GetEquipmentDic;

        foreach (var equipment in equipmentDic.Values)
            data.AvailableEquipmentList.Add(equipment.Index);
    }
    int GetAvailableEquipCount()
    {
        var availableSlotCount = 6;
        foreach (var equip in data.OwnedEquipmentDic)
        {
            if (equip.Value == 6)
                availableSlotCount--;
        }
        return Mathf.Min(availableSlotCount, data.AvailableEquipmentList.Count);
    }
    public HashSet<long> GetRandomEquipmentSet(int count)
    {
        data.SelectedEquipmentSet.Clear();

        while (data.SelectedEquipmentSet.Count < count)
        {
            int randomIndex = Random.Range(0, data.AvailableEquipmentList.Count);
            data.SelectedEquipmentSet.Add(data.AvailableEquipmentList[randomIndex]);
        }
        return data.SelectedEquipmentSet;
    }
    public void SelectEquipment(long index)
    {
        if (data.OwnedEquipmentDic.ContainsKey(index) == false)
            RegisterEquipment(index);
        else
            LevelUpEquipment(index);

        owner.Status.RecomputeStatus(DataManager.EquipmentTable[index].TargetStatus);
    }
    void RegisterEquipment(long index)
    {
        data.OwnedEquipmentDic.Add(index, 1);

        if (data.OwnedEquipmentDic.Count == 6)
            data.AvailableEquipmentList.RemoveAll(equipment => data.OwnedEquipmentDic.ContainsKey(equipment) == false);
    }
    void LevelUpEquipment(long index)
    {
        data.OwnedEquipmentDic[index]+=1;

        if (data.OwnedEquipmentDic[index] >= 6)
            data.AvailableEquipmentList.Remove(index);
    }
    public float GetEquipmentValue(eStatusType type)
    {
        if(type==eStatusType.Revival)
        {
            Debug.Log("error");
            return 0;
        }
        var table = DataManager.EquipmentTable;
        var key = table.GetEquipmentByStatus(type);

        return table[key].GetValue(data.OwnedEquipmentDic.TryGetValue(key, out long level) ? level : 0);
    }
    #endregion

    #region Random Method
    public (int skill, int equip) GenerateRandomPair()
    {
        int skillCont = 0;
        int equipCount = 0;
        
        var availableSkillCount = GetAvailableSkillCount();
        var availableEquipCount = GetAvailableEquipCount();

        if (availableSkillCount + availableEquipCount > GameConst.LevelUpSlotCount)
        {
            int minEquipCount = System.Math.Max(0, GameConst.LevelUpSlotCount - availableSkillCount);
            int maxEquipCount = System.Math.Min(GameConst.LevelUpSlotCount, availableEquipCount);

            equipCount = Random.Range(minEquipCount, maxEquipCount + 1);
            skillCont = GameConst.LevelUpSlotCount - equipCount;
        }
        else
        {
            skillCont = availableSkillCount;
            equipCount = availableEquipCount;
        }

        return (skillCont, equipCount);
    }
    #endregion

    #region Level Method
    void InitLevelData()
    {
        CurrentLevel = 1;
        SetNextLevelExp();
    }
    public void GetExp(float exp)
    {
        CurrentExp += exp* owner.Status.GetStatus(eStatusType.Greed);

        if (CurrentExp >= data.NextLevelExp)
            LevelUp();
    }
    //To DO : 연속 레벨업 고려하기
    void LevelUp()
    {
        CurrentLevel++;
        UIManager.Instance.LevelUpPopUpUI.Enable();
        SetNextLevelExp();
    }
    void SetNextLevelExp()
    {
        var nextLevel = CurrentLevel + 1;
        data.NextLevelExp = 10 + 10 * (nextLevel - 1);
        CurrentExp = 0;
    }
    #endregion
}
