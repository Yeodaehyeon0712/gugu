using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class BaseSkill
{
    #region Fields
    //�Һ�
    protected readonly Data.SkillData skillData;
    public Data.SkillData Data => skillData;
    protected readonly long index;

    //���ϴ� ��
    protected Actor owner;
    protected eSkillState processState;
    public eSkillState ProcessState => processState;
    protected float elapsedTime;

    protected int level=1;
    public int Level => level;
    #endregion

    #region Init Method
    public BaseSkill(long index, Data.SkillData skillData)
    {
        this.index = index;
        this.skillData = skillData;
    }
    #endregion

    #region Skill Method
    public void RegisterSkill(Actor owner)
    {
        this.owner = owner;
        processState = eSkillState.Cooltime;
        elapsedTime = skillData.CoolTime;
        OnRegister(); 
    }
    public void UpdateSkill(float deltaTime)
    {
        elapsedTime += deltaTime;

        if (processState == eSkillState.Cooltime && elapsedTime >= skillData.CoolTime)
            UseSkill();
        else if (processState == eSkillState.Using && elapsedTime >= skillData.DurationTime)
            StopSkill();
    }
    void UseSkill()
    {
        processState = eSkillState.Using;
        elapsedTime = 0;
        OnUsingSequenceAsync().Forget();        
    }
    void StopSkill()
    {
        processState = eSkillState.Cooltime;
        elapsedTime = 0;
        OnStop();
    }
    public void LevelUpSkill(bool isResetTime)
    {
        //StopSkill();
        level++;
        processState = eSkillState.Cooltime;
        elapsedTime = isResetTime ? 0:skillData.CoolTime;
        OnLevelUp();
    }
    public void UnregisterSkill()
    {
        owner = null;
        processState = eSkillState.None;
        elapsedTime = 0;
        level = 0;
        OnUnRegister();
    }
    #endregion

    #region Abstract Method
    protected virtual void OnRegister() { }
    protected abstract UniTask OnUsingSequenceAsync();
    protected virtual void OnStop(){}
    protected virtual void OnLevelUp(){}
    protected virtual void OnUnRegister(){}
    #endregion
}