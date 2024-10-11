using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Fields
    protected Dictionary<eComponent, BaseComponent> componentDictionary = new Dictionary<eComponent, BaseComponent>();

    [SerializeField] protected SkinComponent skinComponent;
    [SerializeField] protected ControllerComponent controllerComponent;

    public SkinComponent Skin => skinComponent;
    public ControllerComponent Controller => controllerComponent;
    #endregion

    #region Unity Method
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (controllerComponent == null) return; 
            controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    protected virtual void LateUpdate()
    {

    }
    #endregion

    #region Actor Method
    public virtual void Initialize()
    {
        // Init Order is Important . Dont Change
        skinComponent = new SkinComponent(this);
        controllerComponent = new ControllerComponent(this);
    }
    #endregion

    #region Component Method
    public void AddComponent(BaseComponent component)
    {
        if(componentDictionary.ContainsKey(component.ComponentType))
        {
            componentDictionary[component.ComponentType].DestroyComponent();
            componentDictionary.Remove(component.ComponentType);
        }
        componentDictionary.Add(component.ComponentType, component);
    }
    protected void OnUpdateComponent(float deltaTime)
    {
        foreach (var component in componentDictionary)
            component.Value.ComponentUpdate(deltaTime);
    }
    #endregion
}
