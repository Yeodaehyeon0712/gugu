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
    public eActorState ActorState => state;
    protected eActorState state;

    //Status Fields
    [SerializeField] public float SetHP { set => currentHP = value; }
    [SerializeField] protected float currentHP;

    //Component Fields
    protected Dictionary<eComponent, BaseComponent> componentDictionary = new Dictionary<eComponent, BaseComponent>();
    public SkinComponent Skin => skinComponent;
    [SerializeField] protected SkinComponent skinComponent;
    public ControllerComponent Controller => controllerComponent;
    [SerializeField] protected ControllerComponent controllerComponent;
    public StatusComponent Status=> statusComponent;
    [SerializeField] protected StatusComponent statusComponent;
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
        base.Spawn(worldID, position);
        ResetComponent();
        state = eActorState.Battle;
        currentHP = statusComponent.GetStatus(eStatusType.MaxHP);
    }
    public virtual void Death()
    {
        state = eActorState.Death;
        currentHP = 0;
        Skin.SetAnimationTrigger(eCharacterAnimState.Death);
        Clean(2.5f);
    }
    protected override void OnClean()
    {
        state = eActorState.Inactive;
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
    public void Recovery(ref AttackHandler attackHandler)
    {
        currentHP = System.Math.Clamp(currentHP - (float)attackHandler.Damage, 0, statusComponent.GetStatus(eStatusType.MaxHP));
        //UIManager.Instance.FieldUI.SetDamageText2(_attachment.GetAttachmentElement(eAttachmentTarget.Body).Transform.position, attackHandler.Damage, attackHandler.IsCritical, _characterType);
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
    public void ResetComponent()
    {
        foreach (var component in componentDictionary)
            component.Value.ResetComponent();
    }
    void UpdateComponent(float deltaTime)
    {
        foreach (var component in componentDictionary)
            component.Value.ComponentUpdate(deltaTime);
    }
    #endregion
}