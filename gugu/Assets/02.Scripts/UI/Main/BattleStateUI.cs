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
        var panel = transform.Find("Panel_BattleState");
        btn_Pause = panel.Find("Panel_Top/Btn_Pause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OpenPausePopUp);
        text_Timer = panel.Find("Panel_Top/Text_Timer").GetComponent<TextMeshProUGUI>();
        text_CoinCount = panel.Find("Panel_Top/Panel_Right/Panel_Coin/Text_CoinCount").GetComponent<TextMeshProUGUI>();
        text_KillCount = panel.Find("Panel_Top/Panel_Right/Panel_Kill/Text_KillCount").GetComponent<TextMeshProUGUI>();
        slider_Exp = panel.Find("Panel_Bottom/Slider_Exp").GetComponent<Slider>();
        text_Level = panel.Find("Panel_Bottom/Panel_Level/Text_Level").GetComponent<TextMeshProUGUI>();
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
    public void SetTimerText(float time)
    {
        int min = (int)(time / 60);  // 정수로 변환
        int second = (int)(time % 60);  // 정수로 변환

        text_Timer.text = $"{min}:{second:D2}";  // 0이 붙도록 포맷
    }
}
