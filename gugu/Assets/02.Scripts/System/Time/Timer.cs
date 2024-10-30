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
    #endregion

    #region Timer Method
    public Timer(float targetTime,Action onTimerComplete, TimerCondition checkCondition=null,Action<float>onUpdate=null)
    {
        this.targetTime = targetTime;
        this.onTimerComplete = onTimerComplete;
        this.checkCondition = checkCondition;
        this.onUpdate = onUpdate;

    }
    public static Timer SetTimer(float targetTime, Action onTimerComplete,bool isFollowMainSpeed, TimerCondition checkCondition = null, Action<float> onUpdate = null)
    {
        Timer timer = new Timer(targetTime, onTimerComplete,checkCondition, onUpdate);
        timer.isFollowMainSpeed = isFollowMainSpeed;
        TimeManager.Instance.AddTimer = timer;
        return timer;
    }
    public void ReStartTimer(float targetTime,float startTime=0)
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

            onUpdate?.Invoke(targetTime - elapsedTime);

            if (targetTime > elapsedTime)
                return true;

            if (onTimerComplete != null)
            {
                if (checkCondition == null) onTimerComplete();
                else if (checkCondition()) onTimerComplete();
            }

            return false;
        }
    }
    #endregion
}
