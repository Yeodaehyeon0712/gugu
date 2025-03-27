using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    #region Scene Method
    protected override void OnStartScene()
    {
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.MenuButton | eUI.Lobby | eUI.PlayerInfo);
    }
    protected override void OnStopScene()
    {
        UIManager.Instance.GameUI.CloseUIByFlag(eUI.MenuButton | eUI.Lobby | eUI.PlayerInfo);
    }
    #endregion
}
