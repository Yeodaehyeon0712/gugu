using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageUI : PopUpUI
{
    #region Fields
    TextMeshProUGUI titleText;
    Button btn_Exit;

    Dictionary<eLanguage, SwitchButton> languageButtonDic;
    [SerializeField] SwitchButton languageButtonResource;
    #endregion

    protected override void InitReference()
    {
        var panel_Title = transform.Find("Panel_Title");
        titleText = panel_Title.Find("Text_Description").GetComponent<TextMeshProUGUI>();
        btn_Exit= panel_Title.Find("Btn_Exit").GetComponent<Button>();
        btn_Exit.onClick.AddListener(() => UIManager.Instance.SettingPopUpUI.SubUI = SettingPopUpUI.eSubUI.None);

        Transform root = transform.Find("Scroll View/Viewport/Content");
        languageButtonDic = new Dictionary<eLanguage, SwitchButton>((int)eLanguage.End - 1);
        for (eLanguage i = eLanguage.EN; i < eLanguage.End; i++)
        {
            var btn_language = Instantiate(languageButtonResource, root);
            btn_language.TargetGraphic = btn_language.GetComponent<Image>();

            eLanguage currentLanguage = i;
            btn_language.onClick.AddListener(() => OnClickLanguage(currentLanguage));
            languageButtonDic.Add(currentLanguage, btn_language);
            btn_language.GetComponentInChildren<TextMeshProUGUI>(true).text = LocalizingManager.Instance.GetLocalizing(201+(int)i);
            btn_language.SetImage(currentLanguage == RuntimePreference.Data.Language);
        }
    }
    protected override void OnRefresh()
    {
        titleText.text = LocalizingManager.Instance.GetLocalizing(223);
    }
    void OnClickLanguage(eLanguage language)
    {
        if (RuntimePreference.Data.Language == language)
            return;
        languageButtonDic[RuntimePreference.Data.Language].SetImage(isOn: false);
        LocalizingManager.Instance.SetLanguage(language);
        languageButtonDic[RuntimePreference.Data.Language].SetImage(isOn: true);
        UIManager.Instance.SettingPopUpUI.SubUI = SettingPopUpUI.eSubUI.None;
    }
    
}
