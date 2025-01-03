using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BaseEffect : MonoBehaviour
{
    #region Fields
    //Effect Fields
    public bool IsActive => isActive;
    bool isActive;
    public uint WorldID => worldID;
    [SerializeField] protected uint worldID;
    public int Index => index;
    [SerializeField] protected int index;
    [SerializeField] float elapsedTime;
    [SerializeField] protected float durationTime;

    //Builder Fields
    [Space]
    [Header("Builder Attribute")]
    [SerializeField] eEffectAttribute builderAttribute;

    [Header("Velocity")]
    [SerializeField] float speed;
    Vector2 initPosition;
    Vector2 targetPosition;
    float elapsedLerpTime;
    float distance;

    [Header("Overlap")]
    [SerializeField] eActorType overlapTargetType;

    [Header("Post Effect")]
    [SerializeField]ePostEffectType postEffectType;
    [SerializeField] float postEffectValue;
    #endregion

    #region Effect Mehtod
    public virtual BaseEffect Initialize(int index)
    {
        this.index = index;
        //this.soundIndex = DataManager.EffectTable[index].SoundIndex;
        return this;
    }
    public virtual void Enable(uint worldID, Vector3 position, float durationTime)
    {
        isActive = true;

        this.worldID = worldID;
        this.durationTime = durationTime;
        elapsedTime = 0;
        transform.position = position;

        //SoundManager.Instance.PlaySFXMusic(soundIndex);
        OnEventChainEffect(eEffectChainCondition.Enable);
        gameObject.SetActive(true);
    }
    public virtual void Disable(bool isRegist=true)
    {
        if (isActive==false) return;

        isActive = false;
        builderAttribute = 0;
        //액터풀로 돌려보내기
        OnEventChainEffect(eEffectChainCondition.Disable);
        gameObject.SetActive(false);
    }
    void OnEventChainEffect(eEffectChainCondition chain)
    {
        //if (DataManager.EffectTable[index].EffectChainDictionary == null) return;

        //if (DataManager.EffectTable[index].EffectChainDictionary.ContainsKey(chain) == false) return;

        //int effectKey = DataManager.EffectTable[index].EffectChainDictionary[chain];
        //EffectManager.Instance.SpawnEffect(effectKey, transform.position, DataManager.EffectTable[_index].EffectChainDurationDictionary[chain]);
    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if (isActive == false|| durationTime == 0) return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime > durationTime)
            Disable();
    }
    private void FixedUpdate()
    {
        if (isActive==false|| TimeManager.Instance.IsActiveTimeFlow == false) return;

        OnUpdateVelocity();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsActive == false || (builderAttribute & eEffectAttribute.Overlap) == 0) return;

        Debug.Log("충돌");
        //if (actor.CharacterType == _overlapTargetType && actor.FSMState != eFSMState.Death)
        //{
        //    AttackHandler attackHandler = new AttackHandler(_casterID, actor.WorldID, _damage, _isCritical);
        //    ActorManager.Instance.PushAttackHandler = attackHandler;
        //    OnEventChainEffect(eEffectChainCondition.Overlap);
        //    OverlapChainEvent?.Invoke();
        //    if (_postEffectType != eMissilePostEffectType.None)
        //        OnPostEffect(_casterID, actor);

        //    Disable();
        //}
        if ((builderAttribute & eEffectAttribute.PostEffect) != 0) 
            OnPostEffect();

        Disable();
    }
    #endregion

    #region Builder - Velocity
    Vector3 direction;
    public BaseEffect SetVelocity(Transform Target)//추후에 Actor로 변경
    {
        builderAttribute |= eEffectAttribute.Velocity;
        direction = (Target.position- transform.position).normalized;
        return this;
    }
    void OnUpdateVelocity()
    {
        if ((builderAttribute & eEffectAttribute.Velocity) == 0) return;

        //if (_target.FSMState == eFSMState.Death)
        //{
        //    Disable();
        //    return;
        //}
        transform.position  += (direction * speed * Time.fixedDeltaTime);
    }
    #endregion

    #region Bulider - Overlap
    public BaseEffect SetOverlapEvent(eActorType targetType)
    {
        builderAttribute |= eEffectAttribute.Overlap;       
        overlapTargetType = targetType;
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
        Debug.Log("qkftod");
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