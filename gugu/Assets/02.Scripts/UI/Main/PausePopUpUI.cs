using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PausePopUpUI : PopUpUI
{
    #region Variable
    Button btn_Setting;
    Button btn_Chart;

    TextMeshProUGUI text_CoinCount;
    TextMeshProUGUI text_KillCount;

    TextMeshProUGUI text_attackSkillTitle;
    TextMeshProUGUI text_passiveSkillTitle;

    Button btn_Continue;
    TextMeshProUGUI text_Continue;
    Button btn_Exit;
    TextMeshProUGUI text_Exit;

    #endregion

    #region Init Method
    protected override void InitReference()
    {
        var panel_Top = transform.Find("Panel_Top");
        btn_Setting= panel_Top.Find("Btn_Setting").GetComponent<Button>();
        btn_Setting.onClick.AddListener(() => UIManager.Instance.SettingPopUpUI.Enable());
        btn_Chart = panel_Top.Find("Btn_Chart").GetComponent<Button>();
        //btn_Chart.onClick.AddListener(() => UIManager.Instance.SettingPopUpUI.Enable());
        text_CoinCount = panel_Top.Find("Panel_Coin/Text_CoinCount").GetComponent<TextMeshProUGUI>();
        text_KillCount = panel_Top.Find("Panel_Kill/Text_KillCount").GetComponent<TextMeshProUGUI>();

        var panel_Middle = transform.Find("Panel_Middle");
        text_attackSkillTitle = panel_Middle.Find("Panel_AttackSkill/Image_Title/Text_Description").GetComponent<TextMeshProUGUI>(); ;
        text_passiveSkillTitle = panel_Middle.Find("Panel_PassivekSkill/Image_Title/Text_Description").GetComponent<TextMeshProUGUI>(); ;

        var panel_Bottom = transform.Find("Panel_Bottom");
        btn_Continue = panel_Bottom.Find("Btn_Continue").GetComponent<Button>();
        btn_Continue.onClick.AddListener(() => Disable());
        text_Continue = btn_Continue.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        btn_Exit = panel_Bottom.Find("Btn_Exit").GetComponent<Button>();
        btn_Exit.onClick.AddListener(() => StageManager.Instance.StopStage());
        text_Exit = btn_Exit.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
    }
    protected override void OnRefresh()
    {
        text_attackSkillTitle.text = LocalizingManager.Instance.GetLocalizing(211);
        text_passiveSkillTitle.text = LocalizingManager.Instance.GetLocalizing(212);
        text_Continue.text = LocalizingManager.Instance.GetLocalizing(213);
        text_Exit.text = LocalizingManager.Instance.GetLocalizing(214);
    }
    #endregion

    #region Pause PopUp Method
    public override void Enable()
    {
        base.Enable();
        TimeManager.Instance.IsActiveTimeFlow = false;
    }
    public override void Disable()
    {
        base.Disable();
        TimeManager.Instance.IsActiveTimeFlow = true;
    }
    #endregion

}
