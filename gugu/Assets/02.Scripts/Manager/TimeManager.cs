using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : TSingletonMono<TimeManager>
{
    #region Fields
    //Timer
    List<Timer> timerList = new List<Timer>();
    public Timer AddTimer { set { timerList.Add(value); } }
    public Timer RemoveTimer { set { if (timerList.Contains(value)) timerList.Remove(value); } }

    //Time
    public bool IsActiveTimeFlow = true;
    float elasticTimeScale = 1f;

    bool isCurrentFrameTime;
    float deltaTime;
    bool isCurrentFixedFrameTime;
    float fixedDeltaTime;

    public float ElasticTimeScale
    {
        get { return elasticTimeScale; }
        set { elasticTimeScale = value; }
    }
    public static float DeltaTime
    {
        get
        {
            if (Instance.isCurrentFrameTime)
                return Instance.deltaTime;

            Instance.isCurrentFrameTime = true;

            float accelFactor = /*Player.SnapshotDataProperty.Data.AccelMode ? 2f :*/ 1f;
            Instance.deltaTime = Instance.IsActiveTimeFlow ? (Time.unscaledDeltaTime * Instance.ElasticTimeScale * accelFactor) : 0f;
            return Instance.deltaTime;


        }
    }
    public static float FixedDeltaTime
    {
        get
        {
            if (Instance.isCurrentFixedFrameTime)
                return Instance.fixedDeltaTime;

            Instance.isCurrentFixedFrameTime = true;

            float accelFactor = /*Player.SnapshotDataProperty.Data.AccelMode ? 2f :*/ 1f;
            Instance.fixedDeltaTime = Instance.IsActiveTimeFlow ? (Time.fixedDeltaTime* Instance.ElasticTimeScale * accelFactor) : 0f;
            return Instance.fixedDeltaTime;
        }
    }
    #endregion


    protected override void OnInitialize()
    {
        IsLoad=true;
    }
    private void Update()
    {
        for (int i = timerList.Count - 1; i >= 0; --i)
        {
            if (timerList[i].NextFrame==false)
                RemoveTimer = timerList[i];
        }
    }
    private void LateUpdate()
    {
        isCurrentFrameTime = false;
        isCurrentFixedFrameTime = false;
    }
}
