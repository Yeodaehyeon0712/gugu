using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LanguageUI : PopUpUI
{
    TextMeshProUGUI _titleText;

    Dictionary<eLanguage, Button> _languageButtonDic;
    [SerializeField] Transform _languageButtonResource;
    protected override void InitReference()
    {
        //base.InitReference();
        //_titleText = transform.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        ////transform.Find("Btn_Exit").GetComponent<Button>().onClick.AddListener(OnDownShiftLayerLevel);
        //_languageButtonDic = new Dictionary<eLanguage, Button>((int)eLanguage.End - 1);
        //Transform trs = transform.Find("Scroll View/Viewport/Content");

        //for (eLanguage i = 0; i < eLanguage.End - 1; ++i)
        //{
        //    int number = (int)i;
        //    eLanguage temp = i + 1;
        //    Transform element = Instantiate(_languageButtonResource, trs);
        //    Button btn = element.GetComponent<Button>();
        //    btn.TargetGraphic = element.GetComponent<Image>();
        //    btn.gameObject.SetActive(true);
        //    btn.onClick.AddListener(() => OnClickLanguage(temp));
        //    _languageButtonDic.Add(temp, btn);
        //    btn.GetComponentInChildren<TextMeshProUGUI>(true).text = LocalizingManager.Instance.GetLocalizing(179 + number);
        //}

    }
    private void Awake()
    {
        UIManager.Instance.SettingPopUpUI.SubUI = SettingPopUpUI.eSubUI.None;
    }
    void OnClickLanguage(eLanguage language)
    {
        if (RuntimePreference.Preference.Language == language)
            return;
        //_languageButtonDic[RuntimePreference.Preference.Language].SetImage(isOn: false);
        //RuntimePreference.Preference.Language = language;
        //RuntimePreference.Instance.SavePreference();
        //UIManager.Instance.Main.SubUI = UMainUI.eSubUI.None;
        //UIManager.Instance.MainMenu.SubUI = UMainUI.eSubUI.None;
        //UIManager.Instance.PlayerProperty.Refresh();
        //UIManager.Instance.MainMenu.Refresh();
        //UIManager.Instance.Stage.Refresh();
        //UIManager.Instance.Main.Refresh();
        //UIManager.Instance.TagUI.Refresh();
        /*UIManager.Instance.language.Enable();
        UIManager.Instance.language.OnClickSubmit2(language);*/
    }
}
