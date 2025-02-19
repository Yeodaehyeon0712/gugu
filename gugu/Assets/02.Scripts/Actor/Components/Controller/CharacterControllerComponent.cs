using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class CharacterControllerComponent :ControllerComponent,IObserver<Vector2>
{
    #region Fields
    [SerializeField]Vector2 controlValue;
    #endregion

    #region Component Method
    public CharacterControllerComponent(Actor owner):base(owner)
    {

    }
    
    protected override void OnComponentActive()
    {
        UIManager.Instance.ControllerUI.AddObserver(this);
        CameraManager.Instance.RegisterFollowTarget(owner.transform);
        ActorManager.Instance.RegisterSpawnAreaParent(owner.transform);
    }
    protected override void OnComponentInactive()
    {
        base.OnComponentInactive();
        UIManager.Instance.ControllerUI.RemoveObserver(this);
        CameraManager.Instance.RegisterFollowTarget(null);
        ActorManager.Instance.RegisterSpawnAreaParent(null);
    }
    #endregion

    #region Move Method
    protected override void MoveActor(float fixedDeltaTime)
    {
        if (controlValue == Vector2.zero) return;

        Vector2 nextPos = controlValue * speed * fixedDeltaTime;
        rigidBody.MovePosition(rigidBody.position + nextPos);
        //owner.transform.Translate(nextPos);
    }
    public void OnNotify(Vector2 value)
    {
        this.controlValue = value;
        skinComponet.SetAnimationFloat(value.magnitude);
    }
    #endregion
}
