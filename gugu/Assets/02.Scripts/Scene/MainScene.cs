using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainScene : BaseScene
{
    #region Scene Method
    protected override void OnStartScene()
    {
        StageManager.Instance.SetupStage(eStageType.Normal, 1);
    }
    protected override void OnStopScene()
    {
        
    }
    #endregion
}
