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
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath));
        controllerComponent = new CharacterControllerComponent(this);
        statusComponent = new CharacterStatusComponent(this);
    }
}
