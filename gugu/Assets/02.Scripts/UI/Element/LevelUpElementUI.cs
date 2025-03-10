using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpElementUI : BaseUI
{
    #region Fields
    Image image_Icon;
    Button btn_LevelUp;
    TextMeshProUGUI text_Title;
    TextMeshProUGUI text_Level;
    TextMeshProUGUI text_Description;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        image_Icon = transform.Find("Image_Icon").GetComponent<Image>();
        btn_LevelUp = GetComponent<Button>();
        Transform panel_Text = transform.Find("Panel_Text");
        text_Title = panel_Text.Find("Text_Title").GetComponent<TextMeshProUGUI>();
        text_Level = panel_Text.Find("Text_Level").GetComponent<TextMeshProUGUI>();
        text_Description = panel_Text.Find("Text_Description").GetComponent<TextMeshProUGUI>();
    }
    protected override void OnRefresh() { }
    #endregion

    #region BaseUI Method
    public override void Disable()
    {
        ClearElement();
        base.Disable();
    }
    #endregion

    #region ElementMethod
    public void InitSkillElement(BaseSkill skill)
    {
        var skillData = skill.Data;
        //image_Icon.sprite=DataManager.AddressableSystem.//어드레서블 먼저 처리하기 .
        text_Title.text = LocalizingManager.Instance.GetLocalizing(skillData.NameKey);
        text_Description.text = LocalizingManager.Instance.GetLocalizing(skillData.ExplanationKey);
        btn_LevelUp.onClick.AddListener(() => Player.PlayerCharacter.Skill.RegisterSkill(skill.Index));
    }
    public void InitStatusElement()
    {

    }
    void ClearElement()
    {
        btn_LevelUp.onClick.RemoveAllListeners();
    }
    #endregion
}
