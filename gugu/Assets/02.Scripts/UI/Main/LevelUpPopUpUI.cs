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
        base.Enable();
        TimeManager.Instance.IsActiveTimeFlow = false;
        EnableLevelUpElement();
    }
    public override void Disable()
    {
        base.Disable();
        TimeManager.Instance.IsActiveTimeFlow = true;
        DisableLevelUpElement();
    }
    #endregion

    #region LevelUp PopUp Method
    void EnableLevelUpElement()
    {
        int elementCount = 1;
        using (var enumerator = Player.InGameData.GetRandomSkillSet(elementCount).GetEnumerator())
        {
            for (int i = 0; i < elementCount && enumerator.MoveNext(); i++)
            {
                elements[i].InitSkillElement(DataManager.SkillTable[enumerator.Current]);
            }
        }
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

