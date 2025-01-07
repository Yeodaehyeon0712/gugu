using System.Collections.Generic;
using UnityEngine;

public class Actor : PoolingObject
{
    #region Fields
    //Fields
    protected eActorType type;
    public eActorType ActorType => type;
    protected long index;
    public long Index => index;

    //Component Fields
    protected Dictionary<eComponent, BaseComponent> updateComponentDictionary = new Dictionary<eComponent, BaseComponent>();
    [SerializeField] protected SkinComponent skinComponent;
    [SerializeField] protected ControllerComponent controllerComponent;
    public SkinComponent Skin => skinComponent;
    public ControllerComponent Controller=> controllerComponent;

    #endregion

    #region Unity API
    protected virtual void Update()
    {
        OnUpdateComponent(Time.deltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (controllerComponent == null) return;
        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public virtual void Initialize(eActorType type,long index,int pathHash)
    {
        this.type = type;
        this.index = index;
        SetPathHashCode(pathHash);
        InitializeComponent(type);
    }

    public override void Spawn(uint worldID, Vector2 position)
    {
        base.Spawn(worldID,position);
    }
    public virtual void Death()
    {
        Clean(2.5f);
    }
    public void Hit()
    {

    }
    protected override void ReturnToPool()
    {
        ActorManager.Instance.RegisterActorPool(worldID,(uint)type,pathHashCode);
    }
    #endregion

    #region Component Method
    void InitializeComponent(eActorType type)
    {
        // Init Order is Important . Skin -> Controller
        skinComponent = new SkinComponent(this);

        controllerComponent = type switch
        {
            eActorType.Character => new CharacterControllerComponent(this),
            eActorType.Enemy => new EnemyControllerComponent(this),
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
