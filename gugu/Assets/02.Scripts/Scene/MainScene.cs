using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainScene : BaseScene
{
    public override void StartScene()
    {
        UIManager.Instance.GameUI.OpenUIByFlag(eUI.Controller | eUI.Main);
        var a = GameObject.Find("Actor").GetComponent<Actor>();
        a.Initialize();
    }


}
