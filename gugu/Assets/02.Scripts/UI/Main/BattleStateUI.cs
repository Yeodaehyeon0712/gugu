using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;

public class BattleStateUI : BaseUI
{
    #region Field
    Button btn_Pause;
    TextMeshProUGUI text_Timer;
    TextMeshProUGUI text_CoinCount;
    TextMeshProUGUI text_KillCount;
    Slider slider_Exp;
    TextMeshProUGUI text_Level;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        btn_Pause = transform.Find("Panel_Top/Btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OpenPausePopUp);
        text_Timer = transform.Find("Panel_Top/Text_Time").GetComponent<TextMeshProUGUI>();
        text_CoinCount = transform.Find("Panel_Right/Panel_Coin/Text_CoinCount").GetComponent<TextMeshProUGUI>();
        text_KillCount = transform.Find("Panel_Right/Panel_Kill/Text_KillCount").GetComponent<TextMeshProUGUI>();
        slider_Exp = transform.Find("Panel_Bottom/Slider_Exp").GetComponent<Slider>();
        text_Level = transform.Find("Panel_Bottom/Panel_Level/Text_Level").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
    }
    #endregion

    public void OpenPausePopUp()
    {
        //이건 enum 형으로 처리할수도 있음 고민 .
        UIManager.Instance.PausePopUpUI.Enable();
    }
}
