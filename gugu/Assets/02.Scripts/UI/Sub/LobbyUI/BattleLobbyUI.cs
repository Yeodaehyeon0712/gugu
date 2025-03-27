using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleLobbyUI : BaseUI
{
    #region Fields
    Button btn_Start;
    #endregion

    #region Inut Method
    protected override void InitReference()
    {
        var panel_Bottom = transform.Find("Panel_Bottom");
        btn_Start = panel_Bottom.Find("Btn_Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(StartStage);
    }

    protected override void OnRefresh()
    {

    }
    #endregion
    public void OnEnable()
    {
        btn_Start.enabled = true;
    }
    public void StartStage()
    {
        SceneManager.Instance.AsyncSceneChange<MainScene>().Forget();
        btn_Start.enabled = false;
    }
}
