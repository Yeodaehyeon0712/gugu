using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinComponent : BaseComponent
{
    #region Fields
    SpriteRenderer renderer;//�Ƹ� �Ⱦ��� ..
    Animator animator;

    //Animator Hash
    int speedHash;
    int hitHash;
    int deathHash;

    Dictionary<eCharacterAnimState, int> animatorHashDic = new Dictionary<eCharacterAnimState, int>();
    eCharacterAnimState currentAnim;
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
    //ó�� �ʱ�ȭ �Ҷ� �� .
    public void SetSkin(int index)
    {
        animator = AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath);
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
    #endregion
}
