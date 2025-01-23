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
        statusComponent = new CharacterStatusComponent(this);
        controllerComponent = new CharacterControllerComponent(this);
    }
}
