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
    public void InitElement(long index, bool isSkill)
    {
        var (titleKey, explanationKey, selectAction) = GetElementData(index, isSkill);

        text_Title.text = LocalizingManager.Instance.GetLocalizing(titleKey);
        text_Description.text = LocalizingManager.Instance.GetLocalizing(explanationKey);
        btn_LevelUp.onClick.AddListener(() =>
        {
            selectAction.Invoke();
            UIManager.Instance.LevelUpPopUpUI.Disable();
        });

        Enable();
    }
    private (int, int, System.Action) GetElementData(long index, bool isSkill)
    {
        if (isSkill)
        {
            var skillData = DataManager.SkillTable[index].Data;
            return (skillData.NameKey, skillData.ExplanationKey, () => Player.InGameData.SelectSkill(index));
        }
        else
        {
            var equipData = DataManager.EquipmentTable[index];
            return (equipData.NameKey, equipData.ExplanationKey, () => Player.InGameData.SelectEquipment(index));
        }
    }
    void ClearElement()
    {
        btn_LevelUp.onClick.RemoveAllListeners();
    }
    #endregion
    
}
