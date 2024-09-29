using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public UMainUI Main;
    public UControllerUI Controller;
    public USettingUI Setting;

    public void Initialize()
    {
        Transform safeArea = transform.Find("USafeArea");

        var groupMainUI = safeArea.Find("Group_MainUI");
        Main = groupMainUI.Find("UMainUI").GetComponent<UMainUI>();
        Main.Initialize();
        Controller = groupMainUI.Find("UControllerUI").GetComponent<UControllerUI>();
        Controller.Initialize();

        var groupPopUp = safeArea.Find("Group_PopUp");
        Setting = groupPopUp.Find("USettingUI").GetComponent<USettingUI>();
        Setting.Initialize();
    }    
}
