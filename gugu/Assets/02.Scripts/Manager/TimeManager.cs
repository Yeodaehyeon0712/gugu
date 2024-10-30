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
    bool isCurrentFrameTime;
    float deltaTime;
    float elasticTimeScale = 1f;

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

            if (Instance.IsActiveTimeFlow==false)               
                return Instance.deltaTime = 0f;

            float accelFactor = /*Player.SnapshotDataProperty.Data.AccelMode ? 2f :*/ 1f;
            Instance.deltaTime = Time.unscaledDeltaTime * Instance.ElasticTimeScale * accelFactor ;

            return Instance.deltaTime;
        }
    }
    #endregion


    protected override void OnInitialize()
    {
        IsLoad=true;
    }
    private void Update()
    {
        isCurrentFrameTime = false;

        for (int i = timerList.Count - 1; i >= 0; --i)
        {
            if (timerList[i].NextFrame==false)
                RemoveTimer = timerList[i];
        }
    }
}
