using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    #region Fields
    //Component Fields
    protected Dictionary<eComponent, BaseComponent> updateComponentDictionary = new Dictionary<eComponent, BaseComponent>();
    [SerializeField] protected SkinComponent skinComponent;
    [SerializeField] protected ControllerComponent controllerComponent;
    public SkinComponent Skin => skinComponent;
    public ControllerComponent Controller=> controllerComponent;

    //Fields
    protected eActorType type;
    protected int spawnHashCode;
    #endregion

    #region Unity Method
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
    }
    protected virtual void LateUpdate()
    {
        if (controllerComponent == null) return;
        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Initialize(eActorType type,int spawnHashCode)
    {
        this.spawnHashCode = spawnHashCode;
        this.type = type;
        InitializeComponent(type);
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
    void InitializeComponent(eActorType type)
    {
        skinComponent = new SkinComponent(this);

        controllerComponent = type switch
        {
            eActorType.Character => new CharacterControllerComponent(this),
            eActorType.Enemy => new CharacterControllerComponent(this),
            _ => null
        };
    }
    public void AddComponent(BaseComponent component)
    {
        if(updateComponentDictionary.ContainsKey(component.ComponentType))
        {
            updateComponentDictionary[component.ComponentType].DestroyComponent();
            updateComponentDictionary.Remove(component.ComponentType);
        }
        updateComponentDictionary.Add(component.ComponentType, component);
    }
    protected void OnUpdateComponent(float deltaTime)
    {
        foreach (var component in updateComponentDictionary)
            component.Value.ComponentUpdate(deltaTime);
    }
    #endregion
}
