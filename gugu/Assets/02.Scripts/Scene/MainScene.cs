using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainScene : BaseScene
{
    public override void StartScene()
    {
        StageManager.Instance.SetupStage(eStageType.Normal, 1);
    }
}
