using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    [SerializeField] protected CharacterControllerComponent controllerComponent;
    [SerializeField] protected StatComponent statComponent;

    public CharacterControllerComponent Controller => controllerComponent;
    public StatComponent Stat => statComponent;
    #endregion

    protected void FixedUpdate()
    {
        if (controllerComponent == null) return;
        controllerComponent.FixedComponentUpdate(Time.fixedDeltaTime);
    }
    public override void Initialize(int spawnHashCode)
    {
        base.Initialize(spawnHashCode);
        controllerComponent = new CharacterControllerComponent(this);
        statComponent = new StatComponent(this);
    }
}
