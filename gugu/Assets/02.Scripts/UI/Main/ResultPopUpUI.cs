using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResultPopUpUI : PopUpUI
{
    #region Variables
    TextMeshProUGUI text_Result;
    TextMeshProUGUI text_SurvivalTitle;
    TextMeshProUGUI text_SurvivalTime;
    TextMeshProUGUI text_RecordTitle;

    Button btn_Confirm;
    TextMeshProUGUI text_ConfirmBtnTitle;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        text_Result = transform.Find("Image_Title/Text_Description").GetComponent<TextMeshProUGUI>();
        
        var panel_SurvivalTime = transform.Find("Panel_Middle/Panel_ServieTime");
        text_SurvivalTitle = panel_SurvivalTime.Find("Image_Title/Text_Description").GetComponent<TextMeshProUGUI>();
        text_SurvivalTime = panel_SurvivalTime.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        
        var panel_Record = transform.Find("Panel_Middle/Panel_Record");
        text_RecordTitle = panel_Record.Find("Image_Title/Text_Description").GetComponent<TextMeshProUGUI>();

        var panel_Bottom= transform.Find("Panel_Bottom");
        btn_Confirm = panel_Bottom.Find("Btn_Confirm").GetComponent<Button>();
        btn_Confirm.onClick.AddListener(Confirm);
        text_ConfirmBtnTitle = btn_Confirm.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        text_SurvivalTitle.text = LocalizingManager.Instance.GetLocalizing(1);
        text_RecordTitle.text = LocalizingManager.Instance.GetLocalizing(1);
        text_ConfirmBtnTitle.text = LocalizingManager.Instance.GetLocalizing(1);
    }
    #endregion

    public override void Enable()
    {
        base.Enable();
        SetResult();
    }

    void SetResult()
    {
        var result = StageManager.Instance.GetCurrentFrameworkState();
        Debug.Log("e" + result.ToString());
        //StageManager.Instance.GetFramework<e>
        //StageManager.Instance.StopStage();
        //text_Result=Pla
        //text_SurvivalTime=Player.S
    }
    public void Confirm()
    {
        Disable();
        StageManager.Instance.ClearStage();
    }
}
