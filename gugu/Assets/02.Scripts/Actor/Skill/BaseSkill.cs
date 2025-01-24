using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public abstract class BaseSkill
{
    #region Fields
    //readonly Fields
    public long Index => index;
    protected readonly long index;
    public Data.SkillData Data => skillData;
    protected readonly Data.SkillData skillData;

    //Variable Fields
    protected Actor owner;
    protected eSkillState state;
    protected float elapsedTime=0;
    public bool isMaxLevel => ( level >= GameConst.MaxSkillLevel );
    protected int level=1;
    #endregion

    #region Skill Method
    public BaseSkill(long index, Data.SkillData skillData)
    {
        this.index = index;
        this.skillData = skillData;
    }
    public BaseSkill RegisterSkill(Actor owner,bool isResetTime= false)
    {
        this.owner = owner;
        state = eSkillState.Cooltime;
        elapsedTime = isResetTime ? 0 : skillData.CoolTime;
        OnRegister();
        return this;
    }
    public void UnregisterSkill()
    {
        owner = null;
        state = eSkillState.Inactive;
        elapsedTime = 0;
        level = 1;
        OnUnRegister();
    }
    public void UpdateSkill(float deltaTime)
    {
        elapsedTime += deltaTime;

        if (state == eSkillState.Cooltime && elapsedTime >= skillData.CoolTime)
            UseSkill();
        else if (state == eSkillState.Using && elapsedTime >= skillData.DurationTime)
            StopSkill();
    }
    void UseSkill()
    {
        state = eSkillState.Using;
        elapsedTime = 0;
        UsingSequenceAsync().Forget();        
    }
    void StopSkill()
    {
        state = eSkillState.Cooltime;
        elapsedTime = 0;
        OnStop();
    }
    //이 부분은 고민 필요
    public void LevelUpSkill(bool isResetTime=false)
    {
        if (isMaxLevel) return;
        Mathf.Min(++level, GameConst.MaxSkillLevel);
        state = eSkillState.Cooltime;
        elapsedTime = isResetTime ? 0:skillData.CoolTime;
        OnLevelUp();
    }
    #endregion

    #region Abstract Method
    protected virtual void OnRegister() { }
    protected virtual void OnUnRegister(){}
    protected virtual void OnStop(){}
    protected virtual void OnLevelUp(){}
    protected abstract UniTask UsingSequenceAsync();
    #endregion
}