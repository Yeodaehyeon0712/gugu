using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    public override void StartScene()
    {
        UIManager.Instance.MenuButtonUI.Enable();
        UIManager.Instance.LobbyUI.Enable();

    }

}
