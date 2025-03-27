using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    #region Scene Method
    protected override void OnStartScene()
    {
        UIManager.Instance.MenuButtonUI.Enable();
        UIManager.Instance.LobbyUI.Enable();
    }
    protected override void OnStopScene()
    {
        UIManager.Instance.MenuButtonUI.Disable();
        UIManager.Instance.LobbyUI.Disable();
    }
    #endregion
}
