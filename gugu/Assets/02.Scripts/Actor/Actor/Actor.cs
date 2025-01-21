using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : PoolingObject
{
    #region Fields
    //Fields
    public eActorType ActorType => type;
    protected eActorType type;
    public long Index => index;
    protected long index;

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
        UpdateComponent(Time.deltaTime);
    }
    protected virtual void FixedUpdate()
    {
        if (controllerComponent == null) return;
        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    #endregion

    #region Actor Method
    public override void Spawn(uint worldID, Vector2 position)
    {
        base.Spawn(worldID, position);
        ResetComponent();
        currentHP = statusComponent.GetStatus(eStatusType.MaxHP);
    }
    public virtual void Death()
    {
        currentHP = 0;
        Skin.SetAnimationTrigger(eCharacterAnimState.Death);
        Clean(2.5f);
    }
    public virtual void Hit(in AttackHandler attackHandler)
    {
        ////만약 무적 상태라면 리턴 
        double damage = attackHandler.Damage;

        ////여기서 체력 마니어스 ..
        currentHP -= (float)damage;
        ////데미지 텍스트 띄우기 

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

#region Old Code
//public class Actor : PoolingObject
//{
//    #region Fields
//    //Fields
//    protected eActorType type;
//    public eActorType ActorType => type;
//    protected long index;
//    public long Index => index;

//    //체력
//    public double CurrentHP => _currentHP;
//    [SerializeField] protected double _currentHP;
//    [SerializeField] public double SetHP { set => _currentHP = value; }

//    //Component Fields
//    protected Dictionary<eComponent, BaseComponent> updateComponentDictionary = new Dictionary<eComponent, BaseComponent>();
//    [SerializeField] protected SkinComponent skinComponent;
//    [SerializeField] protected ControllerComponent controllerComponent;
//    public SkinComponent Skin => skinComponent;
//    public ControllerComponent Controller=> controllerComponent;

//    #endregion

//    #region Unity API
//    protected virtual void Update()
//    {
//        OnUpdateComponent(Time.deltaTime);
//    }
//    protected virtual void FixedUpdate()
//    {
//        if (controllerComponent == null) return;
//        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
//    }
//    #endregion

//    #region Actor Method
//    public virtual void Initialize(eActorType type,long index,int objectID)
//    {
//        this.type = type;
//        this.index = index;
//        SetObjectID(objectID);
//        InitializeComponent(type);
//    }

//    public override void Spawn(uint worldID, Vector2 position)
//    {
//        base.Spawn(worldID,position);
//    }
//    public virtual void Death()
//    {
//        Clean(2.5f);
//    }
//    public virtual void Hit(in AttackHandler attackHandler)
//    {
//        //만약 무적 상태라면 리턴 
//        double damage = attackHandler.Damage;

//        //여기서 체력 마니어스 ..
//        _currentHP -= damage;
//        //데미지 텍스트 띄우기 

//        if (_currentHP <= 0f)
//            Death();
//        else
//            //hit anim 처리
//            Skin.SetAnimationTrigger(eCharacterAnimState.Hit);
//    }
//    protected override void ReturnToPool()
//    {
//        ActorManager.Instance.RegisterActorPool(worldID,type,objectID);
//    }
//    #endregion

//    #region Component Method
//    void InitializeComponent(eActorType type)
//    {
//        // Init Order is Important . Skin -> Controller
//        skinComponent = new SkinComponent(this);

//        controllerComponent = type switch
//        {
//            eActorType.Character => new CharacterControllerComponent(this),
//            eActorType.Enemy => new EnemyControllerComponent(this),
//            _ => null
//        };
//    }
//    public void AddComponent(BaseComponent component)
//    {
//        if(updateComponentDictionary.ContainsKey(component.ComponentType))
//        {
//            updateComponentDictionary[component.ComponentType].DestroyComponent();
//            updateComponentDictionary.Remove(component.ComponentType);
//        }
//        updateComponentDictionary.Add(component.ComponentType, component);
//    }
//    protected void OnUpdateComponent(float deltaTime)
//    {
//        foreach (var component in updateComponentDictionary)
//            component.Value.ComponentUpdate(deltaTime);
//    }


//    #endregion
//}
#endregion