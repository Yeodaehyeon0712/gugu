using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IngameData
{
    #region Fields
    //Count
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
}
public class IngameDataProperty 
{
    #region Fields
    public IngameData Data => data;
    IngameData data;
    #endregion

    #region Init Method
    public void InitializeIngameData()
    {
        data=new IngameData();
    }
    public void CleanData()
    {
        data.KillCount = 0;
        data.GoldCount = 0;
        data.AvailableSkillList.Clear();
        data.SelectedSkillSet.Clear();
        data.OwnedSkillDic.Clear();
    }
    #endregion

    #region Skill Method
    public void SetAvaiableSkillList()
    {
        var skillDic = DataManager.SkillTable.GetSkillDic;

        foreach (var skill in skillDic.Values)
            data.AvailableSkillList.Add(skill.Index);
    }
    public HashSet<long> GetRandomSkillSet(int count)
    {
        data.SelectedSkillSet.Clear();
        int targetSkillCount = Mathf.Min(count, data.AvailableSkillList.Count);

        while (data.SelectedSkillSet.Count < targetSkillCount)
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
        data.OwnedSkillDic.Add(index, skill.RegisterSkill(Player.PlayerCharacter));

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
    public void ResetSkills()
    {
        foreach (var item in data.OwnedSkillDic)
        {
            item.Value.UnregisterSkill();
        }

        data.OwnedSkillDic.Clear();
        data.AvailableSkillList.Clear();
        data.SelectedSkillSet.Clear();
    }
    #endregion

    #region Equipment Method
    public void SetAvaiableEquipmentList()
    {
        var equipmentDic = DataManager.EquipmentTable.GetEquipmentDic;

        foreach (var equipment in equipmentDic.Values)
            data.AvailableEquipmentList.Add(equipment.Index);
    }
    public HashSet<long> GetRandomEquipmentSet(int count)
    {
        data.SelectedEquipmentSet.Clear();
        int targetEquipmentCount = Mathf.Min(count, data.AvailableEquipmentList.Count);

        while (data.SelectedEquipmentSet.Count < targetEquipmentCount)
        {
            int randomIndex = Random.Range(0, data.AvailableEquipmentList.Count);
            data.SelectedEquipmentSet.Add(data.AvailableEquipmentList[randomIndex]);
        }
        return data.SelectedEquipmentSet;
    }
    public void SelectEquipment
        (long index)
    {
        if (data.OwnedEquipmentDic.ContainsKey(index) == false)
            RegisterEquipment(index);
        else
            LevelUpEquipment(index);
    }
    void RegisterEquipment(long index)
    {
        var skill = DataManager.EquipmentTable[index];
        data.OwnedEquipmentDic.Add(index, 1);

        if (data.OwnedEquipmentDic.Count == 6)
        {
            data.AvailableEquipmentList.RemoveAll(equipment => data.OwnedEquipmentDic.ContainsKey(equipment) == false);
        }
    }
    void LevelUpEquipment(long index)
    {
        var level = data.OwnedEquipmentDic[index];
        data.OwnedEquipmentDic[index]= level++;

        if(level>=6)
            data.AvailableSkillList.Remove(index);
    }
    public void ResetEquipment()
    {
        data.OwnedEquipmentDic.Clear();
        data.AvailableEquipmentList.Clear();
        data.SelectedEquipmentSet.Clear();
    }
    public float GetEquipmentValue(eStatusType type)
    {
        var table = DataManager.EquipmentTable;
        var key = table.GetEquipmentByStatus(type);
        var defaultValue = table[key].CalculateType == eCalculateType.Flat ? 0 : 1;

        return data.OwnedEquipmentDic.TryGetValue(key, out long level) ?
            table[key].GetValue(level) : defaultValue;
    }
    #endregion

    #region Temp
    public int GetAvailableSkillCount()
    {
        var availableCount = 6;

        foreach (var skill in data.OwnedSkillDic)
        {
            if (skill.Value.isMaxLevel)
                availableCount--;
        }

        return availableCount;
    }
    public int GetAvailableEquipCount()
    {
        var availableCount = 6;

        foreach (var equip in data.OwnedEquipmentDic)
        {
            if (equip.Value == 6)
                availableCount--;
        }
        return availableCount;
    }
    public (int skill, int equip) GenerateRandomPair()
    {
        int skillCont = 0;
        int equipCount = 0;

        var availableSkillCount = GetAvailableSkillCount();
        var availableEquipCount = GetAvailableEquipCount();

        if (availableSkillCount + availableEquipCount > 4)
        {
            int minEquipCount = Mathf.Max(0, 4 - availableSkillCount);
            int maxEquipCount = Mathf.Min(4, availableEquipCount);

            equipCount = Random.Range(minEquipCount, maxEquipCount + 1);
            skillCont = 4 - equipCount;
        }
        else
        {
            skillCont = availableSkillCount;
            equipCount = availableEquipCount;
        }

        return (skillCont, equipCount);
    }
    #endregion
}
