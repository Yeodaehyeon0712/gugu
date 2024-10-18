using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    [SerializeField] protected StatComponent statComponent;
    public StatComponent Stat => statComponent;
    #endregion

    #region Character Method
    public override void Initialize(eActorType type,long index,int spawnHashCode)
    {
        base.Initialize(type,index,spawnHashCode);
        statComponent = new StatComponent(this);
    }
    #endregion
}
