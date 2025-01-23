using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BaseComponent 
{
    #region Fields
    public eComponent ComponentType => componentType;
    [SerializeField] protected readonly eComponent componentType;
    public Actor Owner => owner;
    [SerializeField] protected readonly Actor owner;
    public bool UseUpdate { get => useUpdate; set => useUpdate = value; }
    [SerializeField] protected bool useUpdate;
    #endregion

    #region Component Method
    protected BaseComponent(Actor owner, eComponent componentType,bool useUpdate=true)
    {
        this.useUpdate = useUpdate;
        this.owner = owner;
        this.componentType = componentType;
        owner.AddComponent(this);
    }
    public void ComponentUpdate(float deltaTime)
    {
        if (useUpdate == false) return;
        OnComponentUpdate(deltaTime);

    }
    public void FixedComponentUpdate(float fixedDeltaTime)
    {
        if (useUpdate == false) return;
        OnComponentFixedUpdate(fixedDeltaTime);
    }
    public void ActiveComponent()
    {
        OnComponentActive();
    }
    public void InactiveComponent()
    {
        OnComponentInactive();
    }
    public void DestroyComponent()
    {
        OnComponentDestroy();
    }
    #endregion

    #region Virtual Method
    protected virtual void OnComponentUpdate(float deltaTime) { }
    protected virtual void OnComponentFixedUpdate(float fixedDeltaTime) { }
    protected virtual void OnComponentActive() { }
    protected virtual void OnComponentInactive() { }
    protected virtual void OnComponentDestroy() { }
    #endregion
}

