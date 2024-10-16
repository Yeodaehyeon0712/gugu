using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Fields
    protected Dictionary<eComponent, BaseComponent> componentDictionary = new Dictionary<eComponent, BaseComponent>();
    [SerializeField] protected SkinComponent skinComponent;
    public SkinComponent Skin => skinComponent;
    public int SpawnHashCode;
    #endregion

    #region Unity Method
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Initialize()
    {
        skinComponent = new SkinComponent(this,false);       
    }
    public void Spawn(Vector2 position)
    {

    }
    public void Death()
    {

    }
    public void Hit()
    {

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
