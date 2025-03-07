using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPopUpUI : PopUpUI//, IObserver<eLanguage>
{

    #region Fields
    //Field
    TextMeshProUGUI text_title;
    Button btn_Exit;

    //Preference
    Dictionary<ePreference, SlideToggle> preferenceToggleDic=new Dictionary<ePreference, SlideToggle>();
    Preference Preference => RuntimePreference.Data;
    bool isDirty;
    SlideToggle bgmToggle;
    TextMeshProUGUI text_bgm;
    SlideToggle sfxToggle;
    TextMeshProUGUI text_sfx;
    SlideToggle alramToggle;
    TextMeshProUGUI text_alram;
    SlideToggle joystickToggle;
    TextMeshProUGUI text_joystick;
    SlideToggle vibrationToggle;
    TextMeshProUGUI text_vibration;
    SlideToggle effectToggle;
    TextMeshProUGUI text_effect;


    //Account 
    Button btn_Account;
    TextMeshProUGUI text_account;
    Button btn_Language;
    TextMeshProUGUI text_language;
    Button btn_Coupon;
    TextMeshProUGUI text_coupon;
    Button btn_Support;
    TextMeshProUGUI text_support;
    Button btn_Policy;
    TextMeshProUGUI text_policy;
    #region Sub UI - 추후 상속에 대해 고민해보자
    public enum eSubUI
    {
        None,
        Account,
        Language,
        Coupon,
    }
    Dictionary<eSubUI, PopUpUI> subUIDic;
    public PopUpUI GetSubUI(eSubUI uiType) => subUIDic[uiType];
    eSubUI currentSubUI = eSubUI.None;
    public eSubUI SubUI
    {
        get => currentSubUI;
        set
        {
            if (currentSubUI == value) return;

            if (currentSubUI != eSubUI.None || value == eSubUI.None)
            {
                subUIDic[currentSubUI].Disable();
            }

            if (value != eSubUI.None)
            {
                subUIDic[value].Enable();
            }

            currentSubUI = value;
        }
    }
    #endregion

    //UUID
    Button btn_CopyUserID;
    TextMeshProUGUI text_UserID;
    TextMeshProUGUI text_Version;
    #endregion

    #region Init Mehtod
    protected override void InitReference()
    {   
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
            RuntimePreference.Instance.SaveData();
            isDirty = false;
        }
        base.Disable();
    }
    protected override void OnRefresh()
    {
        text_title.text= LocalizingManager.Instance.GetLocalizing(215);

        text_bgm.text = LocalizingManager.Instance.GetLocalizing(216);
        text_sfx.text = LocalizingManager.Instance.GetLocalizing(217);
        text_alram.text = LocalizingManager.Instance.GetLocalizing(218);
        text_joystick.text = LocalizingManager.Instance.GetLocalizing(219);
        text_vibration.text = LocalizingManager.Instance.GetLocalizing(220);
        text_effect.text = LocalizingManager.Instance.GetLocalizing(221);

        text_account.text = LocalizingManager.Instance.GetLocalizing(222);
        text_language.text = LocalizingManager.Instance.GetLocalizing(223);
        text_coupon.text = LocalizingManager.Instance.GetLocalizing(224);
        text_support.text = LocalizingManager.Instance.GetLocalizing(225);
        text_policy.text = LocalizingManager.Instance.GetLocalizing(226);
    }
    #endregion

    #region Preference Method
    void InitPreference()
    {
        Transform panel_title = transform.Find("Panel_SettingPopUp/Panel_Title");
        text_title = panel_title.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        btn_Exit = panel_title.Find("Btn_Exit").GetComponent<Button>();
        btn_Exit.onClick.AddListener(() => Disable());

        Transform panel_preference = transform.Find("Panel_SettingPopUp/Panel_Setting/Panel_Prefrence");

        Transform panel_bgm = panel_preference.Find("Panel_BGSound");
        bgmToggle = panel_bgm.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.BGM,bgmToggle.Initialize(()=>ToggleAction(ePreference.BGM)));
        text_bgm= panel_bgm.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();

        Transform panel_sfx = panel_preference.Find("Panel_SFXSound");
        sfxToggle = panel_sfx.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.SFX, sfxToggle.Initialize(() => ToggleAction(ePreference.SFX)));
        text_sfx = panel_sfx.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();

        Transform panel_alram = panel_preference.Find("Panel_Alram");
        alramToggle = panel_alram.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Alram, alramToggle.Initialize(() => ToggleAction(ePreference.Alram)));
        text_alram = panel_alram.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();

        Transform panel_joystick = panel_preference.Find("Panel_JoyStick");
        joystickToggle = panel_joystick.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.JoyStick, joystickToggle.Initialize(() => ToggleAction(ePreference.JoyStick)));
        text_joystick = panel_joystick.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();

        Transform panel_vibration = panel_preference.Find("Panel_Vibration");
        vibrationToggle = panel_vibration.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Vibration, vibrationToggle.Initialize(() => ToggleAction(ePreference.Vibration)));
        text_vibration = panel_vibration.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();

        Transform panel_effect = panel_preference.Find("Panel_Effect");
        effectToggle = panel_effect.Find("Panel_Toggle/SlideToggle").GetComponent<SlideToggle>();
        preferenceToggleDic.Add(ePreference.Effect, effectToggle.Initialize(() => ToggleAction(ePreference.Effect)));
        text_effect = panel_effect.Find("Panel_Description/Text_Description").GetComponent<TextMeshProUGUI>();
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
        InitSubUI();

        Transform panel_Account = transform.Find("Panel_SettingPopUp/Panel_Setting/Panel_Account");
        btn_Account= panel_Account.Find("Btn_Account").GetComponent<Button>();
        btn_Account.onClick.AddListener(() =>SubUI=eSubUI.Account);
        text_account = btn_Account.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();

        btn_Language = panel_Account.Find("Btn_Language").GetComponent<Button>();
        btn_Language.onClick.AddListener(() => SubUI=eSubUI.Language);
        text_language = btn_Language.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();

        btn_Coupon = panel_Account.Find("Btn_Copon").GetComponent<Button>();
        btn_Coupon.onClick.AddListener(() => SubUI = eSubUI.Coupon);
        text_coupon = btn_Coupon.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();

        btn_Support = panel_Account.Find("Btn_Support").GetComponent<Button>();
        btn_Support.onClick.AddListener(OnSupportButtonClicked);
        text_support = btn_Support.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();


        btn_Policy = panel_Account.Find("Btn_Policy").GetComponent<Button>();
        btn_Policy.onClick.AddListener(OnPolicyButtonClicked);
        text_policy = btn_Policy.transform.Find("Text_Description").GetComponent<TextMeshProUGUI>();
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

    #region Sub UI Method
    void InitSubUI()
    {
        Transform subUI = transform.Find("SubUI");
        subUIDic = new Dictionary<eSubUI, PopUpUI>()
        {
            { eSubUI.Account, subUI.Find("AccountSubUI").GetComponent<AccountUI>().Initialize() as PopUpUI },
            { eSubUI.Language, subUI.Find("LanguageSubUI").GetComponent<LanguageUI>().Initialize() as PopUpUI },
            { eSubUI.Coupon, subUI.Find("CouponSubUI").GetComponent<CouponUI>().Initialize() as PopUpUI },
        };
    }
    #endregion

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
