using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    #region Fields

    #endregion

    #region EnemyMethod

    #endregion
    protected override void InitializeComponent()
    {
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(DataManager.EnemyTable[index].AnimatorPath));
        statusComponent = new EnemyStatusComponent(this);
        controllerComponent = new EnemyControllerComponent(this);
    }
}
