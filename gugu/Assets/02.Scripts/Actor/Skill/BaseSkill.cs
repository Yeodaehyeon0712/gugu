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
    float elapsedTime=0;
    public uint SkillLevel => level;
    protected uint level;
    public bool isMaxLevel => ( level >= GameConst.MaxSkillLevel );
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
        level = 1;
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
    void UseSkill()
    {
        state = eSkillState.Using;
        elapsedTime = 0;
        OnUse();
    }
    public void UpdateSkill(float deltaTime)
    {
        elapsedTime += deltaTime;

        if (state == eSkillState.Using)
        {
            if (elapsedTime >= skillData.DurationTime)
            {
                StopSkill();
            }
            else
            {
                OnUpdate();
            }
        }
        else if (state == eSkillState.Cooltime)
        {
            if (elapsedTime >= skillData.CoolTime)
            {
                UseSkill();
            }
        }
    }
    void StopSkill()
    {
        state = eSkillState.Cooltime;
        elapsedTime = 0;
        OnStop();
    }
    public void LevelUpSkill()
    {
        if (isMaxLevel) return;
        level = System.Math.Min(level + 1, GameConst.MaxSkillLevel);
        OnLevelUp();
    }
    #endregion

    #region Abstract Method
    protected virtual void OnRegister() { }
    protected virtual void OnUnRegister(){}
    protected virtual void OnUse() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnStop() { }
    protected virtual void OnLevelUp(){}
    #endregion
}