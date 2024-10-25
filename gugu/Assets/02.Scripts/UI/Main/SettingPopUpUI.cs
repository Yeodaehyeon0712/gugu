using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPopUpUI : PopUpUI//, IObserver<eLanguage>
{
    
    #region Fields
    //Preference
    Dictionary<ePreference, SlideToggle> preferenceToggleDic=new Dictionary<ePreference, SlideToggle>();
    Preference Preference => RuntimePreference.Preference;
    bool isDirty;
    SlideToggle bgmToggle;
    SlideToggle sfxToggle;
    SlideToggle alramToggle;
    SlideToggle joystickToggle;
    SlideToggle vibrationToggle;
    SlideToggle effectToggle;

    //Account 
    Button btn_Exit;
    Button btn_Account;
    Button btn_Language;
    Button btn_Coupon;
    Button btn_Support;
    Button btn_Policy;

    //UUID
    Button btn_CopyUserID;
    TextMeshProUGUI text_UserID;
    TextMeshProUGUI text_Version;
    #endregion

    #region Init Mehtod
    protected override void InitReference()
    {
        base.InitReference();      
        InitPreference();
        InitAccount();
        InitUserID();
    }
    public override void Enable()
    {
        SetPreference();
        base.Enable();
    }
    public override void Disable()
    {
        if (isDirty)
        {
            RuntimePreference.Instance.SavePreference();
            isDirty = false;
        }
        base.Disable();
    }

    #endregion

    #region Preference Method
    void InitPreference()
    {
        Transform panel_preference = transform.Find("Panel_SettingPopUp/Panel_Setting/Panel_Prefrence");

        bgmToggle = panel_preference.Find("Panel_BGSound/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.BGM,bgmToggle.Initialize(()=>ToggleAction(ePreference.BGM)));
        sfxToggle = panel_preference.Find("Panel_SFXSound/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.SFX, sfxToggle.Initialize(() => ToggleAction(ePreference.SFX)));
        alramToggle = panel_preference.Find("Panel_Alram/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Alram, alramToggle.Initialize(() => ToggleAction(ePreference.Alram)));
        joystickToggle = panel_preference.Find("Panel_JoyStick/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.JoyStick, joystickToggle.Initialize(() => ToggleAction(ePreference.JoyStick)));
        vibrationToggle = panel_preference.Find("Panel_Vibration/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Vibration, vibrationToggle.Initialize(() => ToggleAction(ePreference.Vibration)));
        effectToggle = panel_preference.Find("Panel_Effect/Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Effect, effectToggle.Initialize(() => ToggleAction(ePreference.Effect)));
    }

    void ToggleAction(ePreference preference)
    {
        isDirty = true;
        RuntimePreference.Instance.TogglePreference(preference);
        switch (preference)
        {
            case ePreference.BGM:
                break;
            case ePreference.SFX:
                break;
            case ePreference.Alram:
                break;
            case ePreference.JoyStick:
                UIManager.Instance.ControllerUI.SetControllerAlpha(Preference.JoyStick ? 1 : 0);
                break;
            case ePreference.Vibration:
                break;
            case ePreference.Effect:
                break;
        }
    }
    void SetPreference()
    {
        foreach (var toggle in preferenceToggleDic)
        {
            toggle.Value.IsOn(RuntimePreference.Instance.GetPreference(toggle.Key));
        }
    }
    #endregion

    #region Account Method
    void InitAccount()
    {
        btn_Exit=transform.Find("Panel_SettingPopUp/Panel_Title/Btn_Exit").GetComponent<Button>();
        btn_Exit.onClick.AddListener(() => Disable());

        Transform panel_Account = transform.Find("Panel_SettingPopUp/Panel_Setting/Panel_Account");
        btn_Account= panel_Account.Find("Btn_Account").GetComponent<Button>();
        //btn_Account.onClick.AddListener(() => UIManager.Instance.AlramPopUpUI.Enable());
        btn_Language = panel_Account.Find("Btn_Language").GetComponent<Button>();
        //btn_Language.onClick.AddListener(() => UIManager.Instance.AlramPopUpUI.Enable());
        btn_Coupon = panel_Account.Find("Btn_Copon").GetComponent<Button>();
        //btn_Coupon.onClick.AddListener(() => UIManager.Instance.AlramPopUpUI.Enable());
        btn_Support = panel_Account.Find("Btn_Support").GetComponent<Button>();
        btn_Support.onClick.AddListener(OnSupportButtonClicked);
        btn_Policy = panel_Account.Find("Btn_Policy").GetComponent<Button>();
        btn_Policy.onClick.AddListener(OnPolicyButtonClicked);
    }

    void OnSupportButtonClicked()
    {
        //string address = LocalizingManager.Instance.GetLocalizing(100001);
        //string title = LocalizingManager.Instance.GetLocalizing(100002);
        //string body = LocalizingManager.Instance.GetLocalizing(100003) + LocalizingManager.Instance.GetLocalizing(100004);
        //string signProvider = NetworkManager.Instance.SignProvider.ToString();

        //Utility.SendToMail(address, title, body, NetworkManager.Instance.UserID, signProvider);      
    }
    void OnPolicyButtonClicked()
    {
        //Application.OpenURL("https://www.nugemstudio.com/contact-8");
    }
    #endregion

    #region User ID Method
    void InitUserID()
    {
        Transform panel_UserID = transform.Find("Panel_SettingPopUp/Panel_Setting/Panel_UserID");
        btn_CopyUserID=panel_UserID.Find("Btn_CopyUserID").GetComponent<Button>();
        text_UserID=panel_UserID.Find("Text_UserID").GetComponent<TextMeshProUGUI>();
        text_Version= panel_UserID.Find("Text_Version").GetComponent<TextMeshProUGUI>();
        //텍스트 초기화까지 진행 ..
    }
    void OnCopyButtonClicked()
    {
        
    }
    #endregion
}
//#region Variable
//Button btn_Exit;

//Button btn_Language_Left;
//TextMeshProUGUI text_Language;
//Button btn_Language_Right;

//Slider slider_Sound;

//Slider slider_Light;
//#endregion

//#region Init Method
//protected override void InitReference()
//{
//    base.InitReference();
//    //btn_Exit = transform.Find("Btn_Exit").GetComponent<Button>();
//    //btn_Exit.onClick.AddListener(OnExitBtnClick);

//    //var languageLayout = transform.Find("Panel_Language/Panel_Btn");
//    //btn_Language_Left = languageLayout.Find("Btn_Left").GetComponent<Button>();
//    //btn_Language_Left.onClick.AddListener(() => OnLanguageBtnClick(isLeft: true));
//    //btn_Language_Right = languageLayout.Find("Btn_Right").GetComponent<Button>();
//    //btn_Language_Right.onClick.AddListener(() => OnLanguageBtnClick(isLeft: false));
//    //text_Language = languageLayout.Find("Text_Language").GetComponent<TextMeshProUGUI>();

//    //slider_Sound = transform.Find("Panel_Sound/Slider_Sound").GetComponent<Slider>();
//    //slider_Light = transform.Find("Panel_Light/Slider_Light").GetComponent<Slider>();

//    //LocalizingManager.Instance.AddObserver(this);
//    //SetLanguageUI(LocalizingManager.Instance.CurrentLanguage);
//}
//#endregion

//#region Exit Method
//void OnExitBtnClick()
//{
//    base.Disable();
//}
//#endregion

//#region Language Method
//public void OnLanguageBtnClick(bool isLeft)
//{
//    int currentLanguage = (int)LocalizingManager.Instance.CurrentLanguage;
//    int nextLanguage = isLeft ? currentLanguage - 1 : currentLanguage + 1;
//    LocalizingManager.Instance.SetLanguage(nextLanguage);
//    LocalizingManager.Instance.CurrentLanguage = (eLanguage)nextLanguage;
//}
//public void OnNotify(eLanguage value)
//{
//    SetLanguageUI(value);
//}
//void SetLanguageUI(eLanguage value)
//{
//    int currentLanguage = (int)value;
//    bool canMoveLeft = (currentLanguage != 0);
//    btn_Language_Left.gameObject.SetActive(canMoveLeft);

//    bool canMoveRight = (currentLanguage != ((int)eLanguage.End) - 1);
//    btn_Language_Right.gameObject.SetActive(canMoveRight);

//    text_Language.text = value.ToString();
//}
//#endregion

//#region Sound Method
//#endregion

//#region Light Method
//#endregion
