using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpPopUpUI : PopUpUI
{
    #region Variable
    LevelUpElementUI elementResource;
    LevelUpElementUI[] elements;
    TextMeshProUGUI text_Level;
    TextMeshProUGUI text_Title;
    Transform panel_skillList;
    #endregion

    #region Init Method
    protected override void InitReference()
    {
        elementResource = Resources.Load<LevelUpElementUI>("UI/Element/LevelUpElementUI");
        elements = new LevelUpElementUI[4];
        text_Level = transform.Find("Panel_Top/Panel_Level/Text_Level").GetComponent<TextMeshProUGUI>();
        text_Title= transform.Find("Panel_Middle/Image_Title/Text_Title").GetComponent<TextMeshProUGUI>();
        panel_skillList = transform.Find("Panel_Middle/Panel_LevelUpList");

        for (int i = 0; i < 4; i++)
        {
            LevelUpElementUI element = Instantiate(elementResource, panel_skillList);
            element.Initialize();
            elements[i] = element;
        }
    }
    protected override void OnRefresh()
    {
        text_Level.text = LocalizingManager.Instance.GetLocalizing(1);
    }
    #endregion

    #region Base UI Method
    public override void Enable()
    {
        TimeManager.Instance.IsActiveTimeFlow = false;
        EnableLevelUpElement();
        base.Enable();
    }
    public override void Disable()
    {
        TimeManager.Instance.IsActiveTimeFlow = true;
        DisableLevelUpElement();
        base.Disable();
    }
    #endregion

    #region LevelUp PopUp Method
    void EnableLevelUpElement()
    {
        var (skillCount, equipCount) = GenerateRandomPair(3);
        var skillSet = Player.InGameData.GetRandomSkillSet(skillCount);
        var equipSet = Player.InGameData.GetRandomEquipmentSet(equipCount);

        int elementCount = 0;

        foreach (var skillId in skillSet)
        {
            if (elementCount >= skillCount) break;
            elements[elementCount++].InitElement(skillId, isSkill:true);
        }

        foreach (var equip in equipSet)
        {
            if (elementCount >= equipCount) break;
            elements[elementCount++].InitElement(equip, isSkill: false);
        }
    }
    public (int skill, int equip) GenerateRandomPair(int targetSum)
    {
        int skillCount = Random.Range(0, targetSum + 1); 
        int equipmentCount = targetSum - skillCount; 

        return (skillCount, equipmentCount);
    }
    void DisableLevelUpElement()
    {
        foreach(var element in elements)
        {
            element.Disable();
        }
    }
    #endregion
}

