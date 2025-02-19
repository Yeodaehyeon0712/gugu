using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerComponent :ControllerComponent
{
    #region Fields
    Rigidbody2D target;
    #endregion

    #region Component Method
    public EnemyControllerComponent(Actor owner) : base(owner)
    {

    }
    protected override void OnComponentActive()
    {
        target = Player.PlayerCharacter.GetComponent<Rigidbody2D>();
    }
    protected override void OnComponentInactive()
    {
        base.OnComponentInactive();
        target = null;
    }

    #endregion

    #region Move Method
    protected override void MoveActor(float fixedDeltaTime)
    {
        Vector2 dirVector = target.position - rigidBody.position;
        if (dirVector.sqrMagnitude <= Mathf.Epsilon) return;

        Vector2 nextVector = dirVector * (speed * fixedDeltaTime / dirVector.magnitude);
        rigidBody.MovePosition(rigidBody.position + nextVector);
        rigidBody.velocity = Vector2.zero;
    }
    #endregion
}
