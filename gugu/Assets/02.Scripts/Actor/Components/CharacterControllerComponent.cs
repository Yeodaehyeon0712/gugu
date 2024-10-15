using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterControllerComponent :BaseComponent,IObserver<Vector2>
{
    #region Fields
    [SerializeField]Vector2 controlValue;
    //���� ������Ʈ���� ���� .
    float speed=3;
    Rigidbody2D rigidBody;
    SkinComponent skinComponet;
    #endregion

    #region Component Method
    public CharacterControllerComponent(Actor owner):base(owner,eComponent.ControllerComponent)
    {
        //��� ������ ������ �ؾ��� ..
        UIManager.Instance.ControllerUI.AddObserver(this);
        CameraManager.Instance.RegisterFollowTarget(owner.transform);
        skinComponet = owner.Skin;
        rigidBody = owner.GetComponent<Rigidbody2D>();
    }
    protected override void OnComponentFixedUpdate(float fixedDeltaTime)
    {
        if (controlValue == Vector2.zero) return;
        MoveActor(fixedDeltaTime);
    }

    void MoveActor(float fixedDeltaTime)
    {
        Vector2 nextPos = controlValue * speed * fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + nextPos);
        //owner.transform.Translate(nextPos);
    }
    protected override void OnComponentReset()
    {
        UIManager.Instance.ControllerUI.RemoveObserver(this);
        CameraManager.Instance.RegisterFollowTarget(null);
    }
    void SetSkinComponentAnim(Vector2 value)
    {
        skinComponet.SetAnimationFloat(value.magnitude);
    }
    #endregion

    #region Observer Method
    public void OnNotify(Vector2 value)
    {
        this.controlValue = value;
        SetSkinComponentAnim(value);
    }
    #endregion
}
