using System.Collections.Generic;
using UnityEngine;

//이건 캐릭터 스탯 .. 
[System.Serializable]
public abstract class StatusComponent : BaseComponent
{ 
    public StatusComponent(Actor owner) : base(owner, eComponent.StatComponent,useUpdate:false)
    {
        
    }
    protected override void OnComponentActive()
    {
        SetDefaultStatus();
    }
    public abstract void SetDefaultStatus();
    public abstract float GetStatus(eStatusType type);
}
