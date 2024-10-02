using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ControllerComponent :BaseComponent,IObserver<Vector2>
{
    #region Fields
    [SerializeField]Vector2 controlValue;
    //½ºÅÝ ÄÄÆ÷³ÍÆ®¿¡¼­ ¹ÞÀÚ .
    float speed=3;
    Rigidbody2D rigidBody;
    #endregion

    #region Component Method
    public ControllerComponent(Actor owner):base(owner,eComponent.ControllerComponent)
    {
        UIManager.Instance.ControllerUI.AddObserver(this);
        rigidBody = owner.GetComponent<Rigidbody2D>();
    }
    protected override void OnComponentFixedUpdate(float fixedDeltaTime)
    {
        MoveActor(fixedDeltaTime);
    }

    void MoveActor(float fixedDeltaTime)
    {
        Vector2 nextPos = controlValue * speed * fixedDeltaTime;
        //owner.transform.Translate(nextPos);
        rigidBody.MovePosition(rigidBody.position + nextPos);
    }
    #endregion

    #region Observer Method
    public void OnNotify(Vector2 value)
    {
        this.controlValue = value;
    }
    #endregion
}
