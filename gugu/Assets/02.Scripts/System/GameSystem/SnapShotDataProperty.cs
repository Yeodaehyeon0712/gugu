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
    protected override void SetDefaultValue()
    {
        data = new SnapShotData();
        for (eStatusType type = eStatusType.None+1; type < eStatusType.End; type++)
        {
            data.statusLevelDic[type] = 0;
        }
    }
    public int GetStatusLevel(eStatusType type)
    {
        return data.statusLevelDic[type];
    }
}
