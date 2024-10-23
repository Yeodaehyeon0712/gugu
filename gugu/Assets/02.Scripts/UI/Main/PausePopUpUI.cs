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
    Button btn_Continue;
    Button btn_Exit;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        base.InitReference();
        btn_Setting= transform.Find("Panel_Top/Btn_Setting").GetComponent<Button>();
        btn_Chart = transform.Find("Panel_Top/Btn_Chart").GetComponent<Button>();
        text_CoinCount = transform.Find("Panel_Top/Panel_Coin/Text_CoinCount").GetComponent<TextMeshProUGUI>();
        text_KillCount = transform.Find("Panel_Top/Panel_Kill/Text_KillCount").GetComponent<TextMeshProUGUI>();
        btn_Continue = transform.Find("Panel_Bottom/Btn_Continue").GetComponent<Button>();
        btn_Exit = transform.Find("Panel_Bottom/Btn_Exit").GetComponent<Button>();
    }
    #endregion

}
