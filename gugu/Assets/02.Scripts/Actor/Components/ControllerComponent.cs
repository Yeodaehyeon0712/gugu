using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ControllerComponent :BaseComponent,IObserver<Vector2>
{
    #region Fields
    [SerializeField]Vector2 controlValue;
    //���� ������Ʈ���� ���� .
    float speed=3;
    Rigidbody2D rigidBody;
    #endregion

    #region Component Method
    public ControllerComponent(Actor owner):base(owner,eComponent.ControllerComponent)
    {
        //��� ������ ������ �ؾ��� ..
        UIManager.Instance.ControllerUI.AddObserver(this);
        CameraManager.Instance.RegisterFollowTarget(owner.transform);
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
    protected override void OnComponentReset()
    {
        UIManager.Instance.ControllerUI.RemoveObserver(this);
        CameraManager.Instance.RegisterFollowTarget(null);
    }
    #endregion

    #region Observer Method
    public void OnNotify(Vector2 value)
    {
        this.controlValue = value;
    }
    #endregion
}
