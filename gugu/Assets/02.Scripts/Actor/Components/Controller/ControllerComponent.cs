using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ControllerComponent : BaseComponent
{
    #region Fields
    protected Rigidbody2D rigidBody;
    protected float speed=3;
    #endregion

    #region Component Method
    public ControllerComponent(Actor owner) : base(owner, eComponent.ControllerComponent,false)
    {
        rigidBody = owner.GetComponent<Rigidbody2D>();
    }
    protected override void OnComponentFixedUpdate(float fixedDeltaTime)
    {
       MoveActor(fixedDeltaTime);
    }
    protected virtual void MoveActor(float fixedDeltaTime) { }
    #endregion
}
