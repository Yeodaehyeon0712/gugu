using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AlramPopUpUI : PopUpUI
{
    #region Fields
    Button btn_Confirm;
    Button btn_Refuse;
    TextMeshProUGUI text_AlramTitle;
    TextMeshProUGUI text_AlramDescription;
    #endregion

    #region Initialize Method
    protected override void InitReference()
    {
        btn_Confirm= transform.Find("Panel_Alram/Panel_Bottom/Btn_Confirm").GetComponent<Button>();
        btn_Refuse = transform.Find("Panel_Alram/Panel_Bottom/Btn_Refuse").GetComponent<Button>();
        text_AlramTitle = transform.Find("Panel_Alram/Panel_Title/Text_Description").GetComponent<TextMeshProUGUI>();
        text_AlramDescription = transform.Find("Panel_Alram/Text_Alram").GetComponent<TextMeshProUGUI>();
    }

    protected override void OnRefresh()
    {
        
    }
    #endregion
    // Start is called before the first frame update

}
