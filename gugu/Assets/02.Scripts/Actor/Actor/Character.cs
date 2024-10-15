using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    [SerializeField] protected CharacterControllerComponent controllerComponent;
    public CharacterControllerComponent Controller => controllerComponent;
    #endregion

    protected void FixedUpdate()
    {
        if (controllerComponent == null) return;
        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    public override void Initialize()
    {
        base.Initialize();
        controllerComponent = new CharacterControllerComponent(this);
    }
}
