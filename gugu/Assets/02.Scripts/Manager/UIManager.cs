using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : TSingletonMono<UIManager>
{
    #region Fields
    GameUI _gameUI;
    public GameUI GameUI => _gameUI;

    //Battle Scene UI
    public ControllerUI ControllerUI => _gameUI.Controller;
    public BattleStateUI BattleStateUI => _gameUI.BattleState;

    //PopUp UI
    public PausePopUpUI PausePopUpUI => _gameUI.PausePopUp;
    public AlramPopUpUI AlramPopUpUI => _gameUI.AlramPopUp;
    #endregion

    protected override void OnInitialize()
    {
        //To do : Addressable ·Î º¯°æ
        _gameUI = Instantiate(Resources.Load<GameUI>("UI/GameUI"), transform);
        _gameUI.Initialize();        
        InitCanvas(_gameUI);
        IsLoad = true;
    }
    void InitCanvas(Component root)
    {
        Canvas canvas = root.GetComponent<Canvas>();
        CanvasScaler scaler = root.GetComponent<CanvasScaler>();        

        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080,1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1F;
        }
    }
}
