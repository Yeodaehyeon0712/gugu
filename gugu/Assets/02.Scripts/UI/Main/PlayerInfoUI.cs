using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfoUI : BaseUI
{
    #region Fields
    Button btn_Setting;
    Image image_PlayerAvatar;
    TextMeshProUGUI text_PlayerName;
    //Goods
    TextMeshProUGUI text_GoldCount;
    TextMeshProUGUI text_SilverCount;
    TextMeshProUGUI text_BronzeCount;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        btn_Setting = transform.Find("Btn_Stting").GetComponent<Button>();
        btn_Setting.onClick.AddListener(() => UIManager.Instance.SettingPopUpUI.Enable());
        var panel_PlayerInfo = transform.Find("Panel_PlayerInfo");
        image_PlayerAvatar = panel_PlayerInfo.Find("Image_PlayerAvatar").GetComponent<Image>();
        text_PlayerName = panel_PlayerInfo.Find("Text_PlayerName").GetComponent<TextMeshProUGUI>();
        var panel_Goods = panel_PlayerInfo.Find("Panel_Goods");
        text_GoldCount = panel_Goods.Find("Panel_Gold/Text_Count").GetComponent<TextMeshProUGUI>();
        text_SilverCount = panel_Goods.Find("Panel_Silver/Text_Count").GetComponent<TextMeshProUGUI>();
        text_BronzeCount = panel_Goods.Find("Panel_Bronze/Text_Count").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        
    }
    #endregion

}
