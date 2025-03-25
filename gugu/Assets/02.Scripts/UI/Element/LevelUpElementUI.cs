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
        if (isSkill)
            SetSkillElemet(index);
        else
            SetEquipElemet(index);
        
        Enable();
    }
    void SetSkillElemet(long index)
    {
        var skillData = DataManager.SkillTable[index].Data;
        var skillLevel = Player.InGameData.Data.OwnedSkillDic.TryGetValue(index, out var skill)?skill.SkillLevel:1;

        text_Title.text = LocalizingManager.Instance.GetLocalizing(skillData.NameKey);
        text_Description.text = skillData.GetExplanationString(skillLevel);
        text_Level.text = $"Lv{skillLevel}";//로컬라이징 매니저로 변경 예정
        //image_Icon.sprite = AddressableSystem.GetIcon(skillData.IconPath);

        btn_LevelUp.onClick.AddListener(() =>
        {
            Player.InGameData.SelectSkill(index);
            UIManager.Instance.LevelUpPopUpUI.Disable();
        });

    }
    void SetEquipElemet(long index)
    {
        var equipData = DataManager.EquipmentTable[index];
        var equipLevel = Player.InGameData.Data.OwnedEquipmentDic.TryGetValue(index, out var level) ? level : 1;

        text_Title.text = LocalizingManager.Instance.GetLocalizing(equipData.NameKey);
        text_Description.text = LocalizingManager.Instance.GetLocalizing(equipData.ExplanationKey,equipData.GetValue(equipLevel));
        text_Level.text= $"Lv{equipLevel}";//로컬라이징 매니저로 변경 예정
        image_Icon.sprite = AddressableSystem.GetIcon(equipData.IconPath);

        btn_LevelUp.onClick.AddListener(() =>
        {
            Player.InGameData.SelectEquipment(index);
            UIManager.Instance.LevelUpPopUpUI.Disable();
        });
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
