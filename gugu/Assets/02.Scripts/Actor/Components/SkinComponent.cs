using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkinComponent : BaseComponent
{
    #region Fields
    SpriteRenderer renderer;//아마 안쓸듯 ..
    Animator animator;
    CircleCollider2D collisionCollider;
    CircleCollider2D triggerCollider;
    float defaultColliderRadius;

    //Animator Hash
    int speedHash;
    int hitHash;
    int deathHash;

    Dictionary<eCharacterAnimState, int> animatorHashDic = new Dictionary<eCharacterAnimState, int>();
    eCharacterAnimState currentAnim;
    #endregion

    #region Component Method
    public SkinComponent(Actor owner, RuntimeAnimatorController controller) : base(owner, eComponent.SkinComponent,useUpdate:false)
    {
        var skin = owner.transform.Find("Skin");
        renderer = skin.GetComponent<SpriteRenderer>();
        animator = skin.GetComponent<Animator>();
        animator.runtimeAnimatorController = controller;
        collisionCollider = skin.GetComponent<CircleCollider2D>();
        triggerCollider = owner.transform.Find("TriggerCollider").GetComponent<CircleCollider2D>();
        defaultColliderRadius = triggerCollider.radius;

        speedHash = Animator.StringToHash("Speed");
        animatorHashDic.Add(eCharacterAnimState.Move, speedHash);
        animatorHashDic.Add(eCharacterAnimState.Idle, speedHash);

        hitHash = Animator.StringToHash("Hit");
        animatorHashDic.Add(eCharacterAnimState.Hit, hitHash);

        deathHash = Animator.StringToHash("Death");
        animatorHashDic.Add(eCharacterAnimState.Death, deathHash);
    }
    protected override void OnComponentActive()
    {
        collisionCollider.enabled = true;
        triggerCollider.radius = defaultColliderRadius;
        owner.Status.RegisterRecomputeAction(eStatusType.Magnet, scaleFactor => ScaleTriggerColliderSize(scaleFactor));
    }
    protected override void OnComponentInactive()
    {
        collisionCollider.enabled = false;
        SetAnimationTrigger(eCharacterAnimState.Death);
    }

    public void SetAnimationTrigger(eCharacterAnimState state)
    {
        currentAnim = state;
        animator.SetTrigger(animatorHashDic[currentAnim]);
    }
    public void SetAnimationFloat(float value)
    {
        var nextState = (value == 0) ? eCharacterAnimState.Idle : eCharacterAnimState.Move;
        if (nextState == currentAnim) return;

        currentAnim = nextState;
        animator.SetFloat(animatorHashDic[currentAnim], value);
    }
    public void SetSkinColor(Color color)
    {
        renderer.color = color;
    }
    #endregion

    #region TriggerColliderMethod
    public void ScaleTriggerColliderSize(float scaleFactor)
    {
        triggerCollider.radius *= scaleFactor;
    }
    #endregion
}
