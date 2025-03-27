using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonUI :BaseUI
{
    #region Fields
    Dictionary<eLobbyUI, MenuButtonUIElement> buttonDic=new Dictionary<eLobbyUI, MenuButtonUIElement>();
    eLobbyUI currentMenu;

    public eLobbyUI CurrentMenu
    {
        get => currentMenu;
        set
        {
            if (value == currentMenu)
                return;

            buttonDic[currentMenu].FocusOut();
            currentMenu = value;
            buttonDic[currentMenu].FocusOn();
            UIManager.Instance.LobbyUI.CurrentLobby = currentMenu;
        }
    }
    #endregion

    #region Init Method
    public override void Enable()
    {
        base.Enable();
        CurrentMenu = eLobbyUI.Battle;
    }
    protected override void InitReference()
    {
        var panel_btn = transform.Find("Panel_Button");

        var btn_Store = panel_btn.Find("Btn_Store").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eLobbyUI.Store, btn_Store.InitElement(() => CurrentMenu = eLobbyUI.Store));

        var btn_Temp1 = panel_btn.Find("Btn_Temp1").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eLobbyUI.Temp1, btn_Temp1.InitElement(() => CurrentMenu = eLobbyUI.Temp1));

        var btn_Battle = panel_btn.Find("Btn_Battle").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eLobbyUI.Battle, btn_Battle.InitElement(() => CurrentMenu = eLobbyUI.Battle));

        var btn_Temp2 = panel_btn.Find("Btn_Temp2").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eLobbyUI.Temp2, btn_Temp2.InitElement(() => CurrentMenu = eLobbyUI.Temp2));

        var btn_Enforce = panel_btn.Find("Btn_Enforce").GetComponent<MenuButtonUIElement>();
        buttonDic.Add(eLobbyUI.Enforce, btn_Enforce.InitElement(() => CurrentMenu = eLobbyUI.Enforce));
    }
    protected override void OnRefresh()
    {
        
    }
    #endregion
}
