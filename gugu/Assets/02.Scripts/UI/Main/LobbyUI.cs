using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : BaseUI
{
    #region Fields
    Dictionary<eLobbyUI,BaseUI> lobbySubUI = new Dictionary<eLobbyUI, BaseUI>();
    eLobbyUI currentLobby;

    public eLobbyUI CurrentLobby
    {
        get => currentLobby;
        set
        {
            lobbySubUI[currentLobby].Disable();
            currentLobby = value;
            lobbySubUI[currentLobby].Enable();
        }
    }
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        var panel_Lobby = transform.Find("Panel_LobbyUI");
        var battleLobbyUI = panel_Lobby.Find("BattleLobbyUI").GetComponent<BattleLobbyUI>();
        lobbySubUI.Add(eLobbyUI.Battle, battleLobbyUI.Initialize());
        var Temp1LobbyUI = panel_Lobby.Find("Temp1LobbyUI").GetComponent<Temp1LobbyUI>();
        lobbySubUI.Add(eLobbyUI.Temp1, Temp1LobbyUI.Initialize());
        var Temp2LobbyUI = panel_Lobby.Find("Temp2LobbyUI").GetComponent<Temp2LobbyUI>();
        lobbySubUI.Add(eLobbyUI.Temp2, Temp2LobbyUI.Initialize());
        var StoreLobbyUI = panel_Lobby.Find("StoreLobbyUI").GetComponent<StoreLobbyUI>();
        lobbySubUI.Add(eLobbyUI.Store, StoreLobbyUI.Initialize());
        var EnforceLobbyUI = panel_Lobby.Find("EnforceLobbyUI").GetComponent<EnforceLobbyUI>();
        lobbySubUI.Add(eLobbyUI.Enforce, EnforceLobbyUI.Initialize());
    }
    protected override void OnRefresh()
    {

    }
    #endregion

}
