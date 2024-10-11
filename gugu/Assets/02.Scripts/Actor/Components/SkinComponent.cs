using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinComponent : BaseComponent
{
    #region Fields
    SpriteRenderer renderer;//아마 안쓸듯 ..
    Animator animator;

    //Animator Hash
    int speedHash;
    int hitHash;
    int deathHash;

    Dictionary<eCharacterAnimState, int> animatorHashDic = new Dictionary<eCharacterAnimState, int>();
    eCharacterAnimState currentState;
    #endregion

    #region Component Method
    public SkinComponent(Actor owner) : base(owner, eComponent.SkinComponent)
    {
        var skin = owner.transform.Find("Skin");
        renderer = skin.GetComponent<SpriteRenderer>();
        animator = skin.GetComponent<Animator>();

        speedHash = Animator.StringToHash("Speed");
        animatorHashDic.Add(eCharacterAnimState.Move, speedHash);
        animatorHashDic.Add(eCharacterAnimState.Idle, speedHash);

        hitHash = Animator.StringToHash("Hit");
        animatorHashDic.Add(eCharacterAnimState.Hit, hitHash);

        deathHash = Animator.StringToHash("Death");
        animatorHashDic.Add(eCharacterAnimState.Death, deathHash);
    }
    //처음 초기화 할때 셋 .
    public void SetSkin()
    {
        //animator =;
    }
    public void SetAnimationTrigger(eCharacterAnimState state)
    {
        currentState = state;
        animator.SetTrigger(animatorHashDic[currentState]);
    }
    public void SetAnimationFloat(float value)
    {
        var nextState = (value == 0) ? eCharacterAnimState.Idle : eCharacterAnimState.Move;
        if (nextState == currentState) return;

        currentState = nextState;
        animator.SetFloat(animatorHashDic[currentState], value);
    }
    #endregion
}
