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
        var animController = AddressableSystem.GetAnimator(DataManager.EnemyTable[index].AnimatorPath);
        skinComponent = new SkinComponent(this, animController);
        controllerComponent = new EnemyControllerComponent(this);
    }
}
