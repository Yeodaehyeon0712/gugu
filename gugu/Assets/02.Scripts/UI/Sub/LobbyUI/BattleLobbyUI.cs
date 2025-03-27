using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BattleLobbyUI : BaseUI
{
    #region Fields
    Button btn_Start;
    public bool isMove = false;
    #endregion
    protected override void InitReference()
    {
        var panel_Bottom = transform.Find("Panel_Bottom");
        btn_Start = panel_Bottom.Find("Btn_Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(Go);
    }

    protected override void OnRefresh()
    {

    }
    public void Go()
    {
        if (isMove == true) return;
        isMove = true;
        SceneManager.Instance.AsyncSceneChange<MainScene>().Forget();
    }

}
