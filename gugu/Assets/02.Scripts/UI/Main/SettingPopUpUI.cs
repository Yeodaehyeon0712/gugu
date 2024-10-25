using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingPopUpUI : PopUpUI//, IObserver<eLanguage>
{
    
    #region Fields
    //Toggles
    Dictionary<ePreference, SlideToggle> preferenceToggleDic=new Dictionary<ePreference, SlideToggle>();
    SlideToggle bgmToggle;
    SlideToggle sfxToggle;
    SlideToggle alramToggle;
    SlideToggle joystickToggle;
    SlideToggle vibrationToggle;
    SlideToggle effectToggle;

    Preference Preference => RuntimePreference.Preference;
    bool isDirty;
    #endregion

    #region Setting Mehtod
    protected override void InitReference()
    {
        base.InitReference();      
        InitToggle();
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
    void SetPreference()
    {
        foreach (var toggle in preferenceToggleDic)
        {
            toggle.Value.IsOn(RuntimePreference.Instance.GetPreference(toggle.Key));
        }
    }
    #endregion

    #region Toggle Method
    void InitToggle()
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
