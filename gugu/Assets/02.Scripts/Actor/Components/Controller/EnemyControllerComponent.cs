using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerComponent :ControllerComponent
{
    Rigidbody2D target;
    #region Component Method
    public EnemyControllerComponent(Actor owner) : base(owner)
    {
        target = Player.PlayerCharacter.GetComponent<Rigidbody2D>();
    }
    #endregion

    protected override void MoveActor(float fixedDeltaTime)
    {
        Vector2 dirVector = target.position - rigidBody.position;
        if (dirVector.sqrMagnitude <= Mathf.Epsilon) return;

        Vector2 nextVector = dirVector * (speed * fixedDeltaTime / dirVector.magnitude);
        rigidBody.MovePosition(rigidBody.position + nextVector);
        rigidBody.velocity = Vector2.zero;
    }

}
