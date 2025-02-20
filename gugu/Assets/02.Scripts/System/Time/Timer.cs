using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate bool TimerCondition();
public class Timer 
{
    #region Fields
    bool isFollowMainSpeed;
    float targetTime;
    float elapsedTime;

    Action<float> onUpdate;
    TimerCondition checkCondition;
    Action onTimerComplete;
    public bool IsOverTime => (targetTime <= elapsedTime);
    #endregion

    #region Timer Method
    public Timer(float targetTime, bool isFollowMainSpeed, Action onTimerComplete = null, TimerCondition checkCondition=null,Action<float>onUpdate=null)
    {
        this.targetTime = targetTime;
        this.isFollowMainSpeed = isFollowMainSpeed;
        this.onTimerComplete = onTimerComplete;
        this.checkCondition = checkCondition;
        this.onUpdate = onUpdate;
    }
    public static Timer SetTimer(float targetTime, bool isFollowMainSpeed, Action onTimerComplete = null, TimerCondition checkCondition = null, Action<float> onUpdate = null)
    {
        Timer timer = new Timer(targetTime, isFollowMainSpeed, onTimerComplete,checkCondition, onUpdate);
        TimeManager.Instance.AddTimer = timer;
        return timer;
    }
    public void StartTimer(float targetTime,float startTime=0)
    {
        this.targetTime = targetTime;
        this.elapsedTime = startTime;
        TimeManager.Instance.AddTimer = this;
    }
    public virtual bool NextFrame
    {
        get
        {
            if (isFollowMainSpeed) elapsedTime += TimeManager.DeltaTime;
            else elapsedTime += Time.deltaTime;

            //onUpdate?.Invoke(targetTime - elapsedTime);
            onUpdate?.Invoke(elapsedTime);

            if (targetTime > elapsedTime)
                return true;

            if (onTimerComplete!=null&&(checkCondition == null || checkCondition()))
            {
                onTimerComplete.Invoke();
            }
            return false;
        }
    }
    #endregion
}
