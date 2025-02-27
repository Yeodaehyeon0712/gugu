using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    #region Fields
    Dictionary<eUI, BaseUI> uiDic = new Dictionary<eUI,BaseUI>();

    //Battle Scene UI
    public ControllerUI Controller;
    public BattleStateUI BattleState;

    //PopUp UI
    public PausePopUpUI PausePopUp;
    public LevelUpPopUpUI LevelUpPopUp;

    //OverPopUp UI
    public SettingPopUpUI SettingPopUp;
    public AlramPopUpUI AlramPopUp;
    #endregion

    public void Initialize()
    {
        Transform safeArea = transform.Find("SafeArea");
        InitializeSafeArea(safeArea);

        //Battle Scene UI
        var groupBattleSceneUI = safeArea.Find("Group_BattleSceneUI");
        Controller = groupBattleSceneUI.Find("ControllerUI").GetComponent<ControllerUI>();
        uiDic.Add(eUI.Controller, Controller.Initialize());
        BattleState = groupBattleSceneUI.Find("BattleStateUI").GetComponent<BattleStateUI>();
        uiDic.Add(eUI.BattleState,BattleState.Initialize());

        //PopUp UI
        var groupPopUp = safeArea.Find("Group_PopUp");
        PausePopUp = groupPopUp.Find("PausePopUpUI").GetComponent<PausePopUpUI>();
        uiDic.Add(eUI.BattlePausePopUp,PausePopUp.Initialize());
        LevelUpPopUp= groupPopUp.Find("LevelUpPopUpUI").GetComponent<LevelUpPopUpUI>();
        uiDic.Add(eUI.LevelUpPopUp, LevelUpPopUp.Initialize());

        //OverPopUp UI
        var groupOverPopUp = safeArea.Find("Group_OverPopUp");
        SettingPopUp = groupOverPopUp.Find("SettingPopUpUI").GetComponent<SettingPopUpUI>();
        uiDic.Add(eUI.SettingPopUp, SettingPopUp.Initialize());
        AlramPopUp= groupOverPopUp.Find("AlramPopUpUI").GetComponent<AlramPopUpUI>();
        uiDic.Add(eUI.AlramPopUp, AlramPopUp.Initialize());
    }
    void InitializeSafeArea(Transform safeArea)
    {
        SafeArea safeAreaUI = safeArea.GetComponent<SafeArea>();

        LetterBox letterBox_Top= transform.Find("LetterBox_Top").GetComponent<LetterBox>();
        letterBox_Top.Initialize(safeAreaUI);

        LetterBox letterBox_Bottom = transform.Find("LetterBox_Bottom").GetComponent<LetterBox>();
        letterBox_Bottom.Initialize(safeAreaUI);

        safeAreaUI.Initialize();
    }
    public void OpenUIByFlag(eUI flaggedEnum)
    {       
        foreach (var uiEnum in uiDic.Keys)
        {
            if ((uiEnum & flaggedEnum) == 0) continue;

            uiDic[uiEnum].Enable();
        }
    }
    public void CloseUIByFlag(eUI flaggedEnum)
    {
        foreach (var uiEnum in uiDic.Keys)
        {
            if ((uiEnum & flaggedEnum) == 0) continue;

            uiDic[uiEnum].Disable();
        }
    }
}

