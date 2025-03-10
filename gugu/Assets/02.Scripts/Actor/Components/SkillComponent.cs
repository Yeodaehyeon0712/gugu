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
        SetAvailableSkillList();
        RegisterSkill(DataManager.CharacterTable[owner.ObjectID].DefaultSkillKey);
    }
    protected override void OnComponentInactive()
    {
        ResetSkills();
    }
    protected override void OnComponentUpdate(float deltaTime)
    {
        if (IngameSkillDic.Count < 1) return;

        foreach (var skill in IngameSkillDic.Values)
            skill.UpdateSkill(deltaTime);
    }
    #endregion

    #region Skill Method
    List<long> availableSkillList = new List<long>();//획득 가능한 해금된 스킬 목록
    HashSet<long> selectedSkillSet = new HashSet<long>();//레벨업창에 보여질 목록들
    public Dictionary<long, BaseSkill> IngameSkillDic = new Dictionary<long, BaseSkill>();
    //가능한 스킬들을 처음에 넣는다 .
    public void SetAvailableSkillList()
    {
        foreach (var skill in DataManager.SkillTable.GetSkillDic.Values)
        {
            //만약 스킬이 잠겨있다면 더하기 않는다 
            availableSkillList.Add(skill.Index);
        }
    }
    //렌덤한 스킬을 보여준다
    public HashSet<long> GetRandomSkillSet(int count)
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
    //레벨업창 선택시 해당 메서드 호출
    public void RegisterSkill(long index)
    {
        if (IngameSkillDic.ContainsKey(index) || owner == null) return;

        var skill = DataManager.SkillTable[index];
        IngameSkillDic.Add(index, skill.RegisterSkill(owner));
    }
    //이미 있다면 아래 메서드 호출 . 근데 이건 고민
    public void LevelUpSkill(long index)
    {
        if (IngameSkillDic.TryGetValue(index, out var skill) == false || skill.isMaxLevel) return;
        skill.LevelUpSkill();

        if (skill.isMaxLevel && availableSkillList.Contains(skill.Index))
            availableSkillList.Remove(skill.Index);
    }
    //스킬 리스트를 비운다
    public void ResetSkills()
    {
        foreach (var item in IngameSkillDic)
        {
            item.Value.UnregisterSkill();
        }

        IngameSkillDic.Clear();
        availableSkillList.Clear();
        selectedSkillSet.Clear();
    }
    #endregion
}
