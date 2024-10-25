using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PopUpUI :BaseUI
{
    GameObject popUpBG;
    public override BaseUI Initialize()
    {
        popUpBG = transform.parent.Find("PopUpBG").gameObject;
        return base.Initialize();
    }
    public override void Enable()
    {
        base.Enable();
        popUpBG.SetActive(true);
    }
    public override void Disable()
    {
        base.Disable();
        popUpBG.SetActive(false);
    }
}
