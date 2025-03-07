using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SnapShotData
{
    //능력치 작업 중이었음 . 
    public Dictionary<eStatusType, int> statusLevelDic=new Dictionary<eStatusType, int>();

}
public class SnapShotDataProperty : JsonSerializableData<SnapShotData, SnapShotDataProperty>
{
    public SnapShotDataProperty InitializeSnapShot()
    {
        base.Initialize();
        return this;
    }
    protected override void SetDefaultValue()
    {
        data = new SnapShotData();
        foreach(eStatusType a in System.Enum.GetValues(typeof(eStatusType)))
        {
            data.statusLevelDic[a] = 0;
        }
    }
    public int GetStatusLevel(eStatusType type)
    {
        return data.statusLevelDic[type];
    }
}
