using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterControllerComponent :ControllerComponent,IObserver<Vector2>
{
    #region Fields
    [SerializeField]Vector2 controlValue;
    //스텟 컴포넌트에서 받자 .
    SkinComponent skinComponet;
    #endregion

    #region Component Method
    public CharacterControllerComponent(Actor owner):base(owner)
    {
        //등록 했으면 해제도 해야지 ..
        UIManager.Instance.ControllerUI.AddObserver(this);
        CameraManager.Instance.RegisterFollowTarget(owner.transform);
        ActorManager.Instance.RegisterSpawnAreaParent(owner.transform);
        skinComponet = owner.Skin;
    }
    protected override void MoveActor(float fixedDeltaTime)
    {
        if (controlValue == Vector2.zero) return;

        Vector2 nextPos = controlValue * speed * fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + nextPos);
        //owner.transform.Translate(nextPos);
    }
    protected override void OnComponentReset()
    {
        UIManager.Instance.ControllerUI.RemoveObserver(this);
        CameraManager.Instance.RegisterFollowTarget(null);
        ActorManager.Instance.RegisterSpawnAreaParent(null);
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
