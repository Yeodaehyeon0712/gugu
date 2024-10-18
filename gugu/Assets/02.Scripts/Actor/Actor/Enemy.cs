using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    #region Fields
    [SerializeField]SOEnemyStat stat;
    public SOEnemyStat Stat { get=>stat; set=>stat=value; }
    #endregion

    #region EnemyMethod
    public override void Initialize(eActorType type, long index, int spawnHashCode)
    {
        base.Initialize(type, index, spawnHashCode);
    }
    #endregion
}
