using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BaseComponent 
{
    #region Fields
    [SerializeField] protected eComponent componentType;
    public eComponent ComponentType => componentType;
    [SerializeField] protected bool isActive;
    public bool Active { get => isActive; set => isActive = value; }
    [SerializeField] protected Actor owner;
    public Actor Owner => owner;
    #endregion

    #region Component Method
    protected BaseComponent(Actor owner, eComponent componentType)
    {
        isActive = true;
        this.owner = owner;
        this.componentType = componentType;
        owner.AddComponent(this);
    }
    public void ComponentUpdate(float deltaTime)
    {
        if (isActive == false) return;
        OnComponentUpdate(deltaTime);

    }
    public void FixedComponentUpdate(float fixedDeltaTime)
    {
        if (isActive == false) return;
        OnComponentFixedUpdate(fixedDeltaTime);
    }
    public void ResetComponent()
    {
        OnComponentReset();
    }
    public void DestroyComponent()
    {
        OnComponentDestroy();
    }
    #endregion

    #region Virtual Method
    protected virtual void OnComponentUpdate(float deltaTime) { }
    protected virtual void OnComponentFixedUpdate(float fixedDeltaTime) { }
    protected virtual void OnComponentReset() { }
    protected virtual void OnComponentDestroy() { }
    #endregion
}

