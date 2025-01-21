using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOEnemyStat", menuName = "ScriptableObject/SOEnemyStat", order = int.MinValue)]
public class SOEnemyStatus : ScriptableObject
{
    public float HP;
    public float MoveSpeed;
    public float AttackDamage;

    public static SOEnemyStatus CreateEnemyStatus(Dictionary<string, string> dataPair)
    {
        var instance = CreateInstance<SOEnemyStatus>();
        instance.HP = float.Parse(dataPair["HP"]);
        instance.MoveSpeed = float.Parse(dataPair["MoveSpeed"]);
        instance.AttackDamage = float.Parse(dataPair["AttackDamage"]);
        return instance;
    }
}
