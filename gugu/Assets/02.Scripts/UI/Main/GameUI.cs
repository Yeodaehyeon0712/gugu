using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public UMainUI Main;
    public UControllerUI Controller;
    public USettingUI Setting;
    Dictionary<eUI, UBaseUI> uiDic = new Dictionary<eUI,UBaseUI>();

    public void Initialize()
    {
        Transform safeArea = transform.Find("USafeArea");
        InitializeSafeArea(safeArea);

        var groupMainUI = safeArea.Find("Group_MainUI");
        Main = groupMainUI.Find("UMainUI").GetComponent<UMainUI>();
        uiDic.Add(eUI.Main,Main.Initialize());
        Controller = groupMainUI.Find("UControllerUI").GetComponent<UControllerUI>();
        uiDic.Add(eUI.Controller, Controller.Initialize());

        var groupPopUp = safeArea.Find("Group_PopUp");
        Setting = groupPopUp.Find("USettingUI").GetComponent<USettingUI>();
        uiDic.Add(eUI.Setting,Setting.Initialize());
    }    
    void InitializeSafeArea(Transform safeArea)
    {
        USafeArea safeAreaUI = safeArea.GetComponent<USafeArea>();

        ULetterBox letterBox_Top= transform.Find("LetterBox_Top").GetComponent<ULetterBox>();
        letterBox_Top.Initialize(safeAreaUI);

        ULetterBox letterBox_Bottom = transform.Find("LetterBox_Bottom").GetComponent<ULetterBox>();
        letterBox_Bottom.Initialize(safeAreaUI);

        safeAreaUI.Initialize();
    }
    public void OpenUIByFlag(eUI flaggedEnum)
    {
        foreach(var uiEnum in uiDic.Keys)
        {
            if(uiDic.TryGetValue(uiEnum,out var ui))
            {
                if ((uiEnum & flaggedEnum) != 0)
                    ui.Enable();
                else
                    ui.Disable();
            }
        }
    }
}
[System.Flags]
public enum eUI
{
    Main=1<<0,
    Controller= 1 << 1,
    Setting= 1 << 2,
}
