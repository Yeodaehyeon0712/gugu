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
    List<long> availableSkillList = new List<long>();//ȹ�� ������ �رݵ� ��ų ���
    HashSet<long> selectedSkillSet = new HashSet<long>();//������â�� ������ ��ϵ�
    public Dictionary<long, BaseSkill> IngameSkillDic = new Dictionary<long, BaseSkill>();
    //������ ��ų���� ó���� �ִ´� .
    public void SetAvailableSkillList()
    {
        foreach (var skill in DataManager.SkillTable.GetSkillDic.Values)
        {
            //���� ��ų�� ����ִٸ� ���ϱ� �ʴ´� 
            availableSkillList.Add(skill.Index);
        }
    }
    //������ ��ų�� �����ش�
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
    //������â ���ý� �ش� �޼��� ȣ��
    public void RegisterSkill(long index)
    {
        if (IngameSkillDic.ContainsKey(index) || owner == null) return;

        var skill = DataManager.SkillTable[index];
        IngameSkillDic.Add(index, skill.RegisterSkill(owner));
    }
    //�̹� �ִٸ� �Ʒ� �޼��� ȣ�� . �ٵ� �̰� ���
    public void LevelUpSkill(long index)
    {
        if (IngameSkillDic.TryGetValue(index, out var skill) == false || skill.isMaxLevel) return;
        skill.LevelUpSkill();

        if (skill.isMaxLevel && availableSkillList.Contains(skill.Index))
            availableSkillList.Remove(skill.Index);
    }
    //��ų ����Ʈ�� ����
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
