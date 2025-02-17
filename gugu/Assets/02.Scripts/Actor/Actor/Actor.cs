using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : PoolingObject
{
    #region Fields
    //Fields
    public long Index => index;
    protected long index;
    public eActorType ActorType => type;
    protected eActorType type;
    public eActorState ActorState
    {
        get => state;
        set { state = value; OnStateChange(state); }
    }
    protected eActorState state;
    protected LayerMask targetLayer;


    //Status Fields
    [SerializeField] public float SetHP { set => currentHP = value; }
    [SerializeField] protected float currentHP;

    //Component Fields
    protected Dictionary<eComponent, BaseComponent> componentDictionary = new Dictionary<eComponent, BaseComponent>();
    public SkinComponent Skin => skinComponent;
    [SerializeField] protected SkinComponent skinComponent;
    public StatusComponent Status=> statusComponent;
    [SerializeField] protected StatusComponent statusComponent;
    public ControllerComponent Controller => controllerComponent;
    [SerializeField] protected ControllerComponent controllerComponent;//Controller Must be Init at Last.
    #endregion

    #region Init Method
    public virtual void Initialize(eActorType type, long index, int objectID)
    {
        base.Initialize(objectID);
        this.type = type;
        this.index = index;
        InitializeComponent();
    }
    protected override void ReturnToPool()
    {
        ActorManager.Instance.RegisterActorPool(worldID, type, objectID);
    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if (state != eActorState.Battle) return;
        UpdateComponent(TimeManager.DeltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (controllerComponent == null||state != eActorState.Battle) return;
        controllerComponent.FixedComponentUpdate(TimeManager.FixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public override void Spawn(uint worldID, Vector2 position)
    {
        ActorState = eActorState.Battle;
        base.Spawn(worldID, position);
    }
    public virtual void Death()
    {
        ActorState = eActorState.Death;   
        Clean(2.5f);
    }
    protected override void OnClean()
    {
        ActorState = eActorState.Clean;
        base.OnClean();
    }
    public virtual void Hit(in AttackHandler attackHandler)
    {
        double damage = attackHandler.Damage;
        currentHP -= (float)damage;

        if (currentHP <= 0f)
            Death();
        else
            Skin.SetAnimationTrigger(eCharacterAnimState.Hit);
    }
    public virtual void Hit(double damage)
    {
        currentHP -= (float)damage;


        if (currentHP <= 0f)
            Death();
        else
            Skin.SetAnimationTrigger(eCharacterAnimState.Hit);
    }
    public void Recovery(ref AttackHandler attackHandler)
    {
        currentHP = System.Math.Clamp(currentHP - (float)attackHandler.Damage, 0, statusComponent.GetStatus(eStatusType.MaxHP));
        //UIManager.Instance.FieldUI.SetDamageText2(_attachment.GetAttachmentElement(eAttachmentTarget.Body).Transform.position, attackHandler.Damage, attackHandler.IsCritical, _characterType);
    }
    public void OnStateChange(eActorState state)
    {
        switch (state)
        {
            case eActorState.Battle:
                ActiveComponent();
                currentHP = statusComponent.GetStatus(eStatusType.MaxHP);
                break;

            case eActorState.Death:
                InactiveComponent();
                currentHP = 0;
                break;

            case eActorState.Clean:
                break;
        }
    }
    protected bool CheckTargetLayer(int layer)
    {
        return (targetLayer.value & (1 << layer)) != 0;
    }
    #endregion

    #region Component Method
    protected abstract void InitializeComponent();
    public T GetComponent<T>(eComponent componentType) where T : BaseComponent
    {
        return componentDictionary.ContainsKey(componentType) ? componentDictionary[componentType] as T : null;
    }
    public void AddComponent(BaseComponent component)
    {
        if (componentDictionary.ContainsKey(component.ComponentType))
        {
            componentDictionary[component.ComponentType].DestroyComponent();
            componentDictionary.Remove(component.ComponentType);
        }
        componentDictionary.Add(component.ComponentType, component);
    }
    public void ActiveComponent()
    {
        foreach (var component in componentDictionary.Values)
            component.ActiveComponent();
    }
    public void InactiveComponent()
    {
        foreach (var component in componentDictionary.Values)
            component.InactiveComponent();
    }
    void UpdateComponent(float deltaTime)
    {
        foreach (var component in componentDictionary.Values)
            component.ComponentUpdate(deltaTime);
    }

    #endregion
}