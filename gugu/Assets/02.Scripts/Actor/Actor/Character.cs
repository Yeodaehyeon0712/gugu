using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields

    #endregion

    #region Character Method

    #endregion
    protected override void InitializeComponent()
    {
        var animController = AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath);
        skinComponent = new SkinComponent(this, animController);
        controllerComponent = new CharacterControllerComponent(this);
        statusComponent = new StatusComponent(this);
    }
}
