using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ControllerComponent : BaseComponent
{
    #region Fields
    protected SkinComponent skinComponet;
    protected StatusComponent statusComponet;
    protected Rigidbody2D rigidBody;
    protected float speed=>statusComponet.GetStatus(eStatusType.MoveSpeed);
    #endregion

    #region Component Method
    public ControllerComponent(Actor owner) : base(owner, eComponent.ControllerComponent)
    {
        skinComponet = owner.Skin;
        statusComponet = owner.Status;
        rigidBody = owner.GetComponent<Rigidbody2D>();
        if (skinComponet == null || statusComponet == null)
            Debug.LogWarning("Init Skin/Status Component Before Controller Component");
    }
    protected override void OnComponentFixedUpdate(float fixedDeltaTime)
    {
       MoveActor(fixedDeltaTime);
    }
    protected virtual void MoveActor(float fixedDeltaTime) { }
    #endregion
}
