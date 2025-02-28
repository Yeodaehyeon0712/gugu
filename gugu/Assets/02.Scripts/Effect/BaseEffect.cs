using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BaseEffect : PoolingObject<eEffectType>
{
    #region Fields
    //Effect Fields
    public long Index => index;
    [SerializeField] protected long index;

    //Builder Fields
    [Space]
    [Header("Builder Attribute")]
    [SerializeField] eEffectAttribute builderAttribute;

    [Space][Header("Duration")]
    [SerializeField] protected float durationTime;
    [SerializeField] float elapsedTime;

    [Header("Velocity")]
    [SerializeField] float speed;
    Vector3 direction;

    [Header("Overlap")]
    [SerializeField] eActorType overlapTargetType;
    [SerializeField] uint casterID;
    [SerializeField] double damage;
    [SerializeField] bool isCritical;

    [Header("Post Effect")]
    [SerializeField]ePostEffectType postEffectType;
    [SerializeField] float postEffectValue;
    #endregion

    #region Effect Mehtod
    protected override void ReturnToPool()
    {
        EffectManager.Instance.RegisterToEffectPool(worldID,type, objectID);
    }
    public virtual BaseEffect Initialize(eEffectType type,long index, int objectID)
    {
        base.Initialize(type,objectID);
        this.type = type;
        this.index = index;
        return this;
    }
    public override void Spawn(uint worldID,Vector2 position)
    {
        OnEventChainEffect(eEffectChainCondition.Enable);
        base.Spawn(worldID, position);
    }

    public virtual void Disable()
    {
        builderAttribute = 0;
        OnEventChainEffect(eEffectChainCondition.Disable);
        Clean(2.5f);
    }
    void OnEventChainEffect(eEffectChainCondition chain)
    {
        //if (DataManager.EffectTable[index].EffectChainDictionary == null) return;
        //if (DataManager.EffectTable[index].EffectChainDictionary.ContainsKey(chain) == false) return;

        //int effectKey = DataManager.EffectTable[index].EffectChainDictionary[chain];
        //EffectManager.Instance.SpawnEffect(effectKey, transform.position, DataManager.EffectTable[index].EffectChainDurationDictionary[chain]);
    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if ((builderAttribute & eEffectAttribute.Duration) == 0) return;

        elapsedTime += Time.deltaTime;
        if (elapsedTime > durationTime)
            Disable();
    }
    private void FixedUpdate()
    {
        if ((builderAttribute & eEffectAttribute.Velocity) == 0) return;

        OnUpdateVelocity(TimeManager.DeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((builderAttribute & eEffectAttribute.Overlap) == 0) return;

        var actor = collision.GetComponentInParent<Actor>();
        if (actor == null || actor.Type != overlapTargetType || actor.ActorState == eActorState.Death)
            return;

        AttackHandler attackHandler = new AttackHandler(casterID, actor.WorldID, damage, isCritical);
        ActorManager.Instance.PushAttackHandler = attackHandler;

        OnEventChainEffect(eEffectChainCondition.Overlap);
        //OverlapChainEvent?.Invoke();
        if (((builderAttribute&eEffectAttribute.PostEffect)!=0) && (postEffectType != ePostEffectType.None))
            OnPostEffect();

        if (type == eEffectType.Projectile)
            Disable();
    }
    #endregion

    #region Builder - Duration
    public BaseEffect SetDuration(float durationTime)
    {
        builderAttribute |= eEffectAttribute.Duration;
        this.durationTime = durationTime;
        this.elapsedTime = 0;
        return this;
    }
    #endregion

    #region Builder - Velocity
    public BaseEffect SetVelocity(Actor Target)//추후에 Actor로 변경
    {
        builderAttribute |= eEffectAttribute.Velocity;
        direction = (Target.transform.position- transform.position).normalized;
        return this;
    }
    void OnUpdateVelocity(float deltaTime)
    {
        transform.position  += (direction * speed * deltaTime);
    }
    #endregion

    #region Bulider - Overlap
    public BaseEffect SetOverlapEvent(eActorType targetType,uint casterID, double damage,bool isCritical)
    {
        builderAttribute |= eEffectAttribute.Overlap;
        overlapTargetType = targetType;
        this.casterID = casterID;
        this.damage = damage;
        this.isCritical = isCritical;
        return this;
    }
    #endregion

    #region Builder - Post Effect
    public BaseEffect SetPostEffect(ePostEffectType type, float value)
    {
        builderAttribute |= eEffectAttribute.PostEffect;
        postEffectType = type;
        postEffectValue = value;
        return this;
    }
    void OnPostEffect() 
    {
        switch (postEffectType)
        {
            case ePostEffectType.None:
                break;
            case ePostEffectType.KnockBack:
                break;
            case ePostEffectType.Stun:
                break;
        }
    }
    #endregion
}