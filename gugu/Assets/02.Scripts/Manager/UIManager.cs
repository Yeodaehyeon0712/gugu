using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : TSingletonMono<UIManager>
{
    #region Fields
    public GameUI GameUI => _gameUI;
    GameUI _gameUI;
    public FieldUI FieldUI => fieldUI;
    FieldUI fieldUI;

    //Battle Scene UI
    public ControllerUI ControllerUI => _gameUI.Controller;
    public BattleStateUI BattleStateUI => _gameUI.BattleState;

    //PopUp UI
    public PausePopUpUI PausePopUpUI => _gameUI.PausePopUp;
    public LevelUpPopUpUI LevelUpPopUpUI => _gameUI.LevelUpPopUp;

    //Over PopUp UI
    public SettingPopUpUI SettingPopUpUI => _gameUI.SettingPopUp;
    public AlramPopUpUI AlramPopUpUI => _gameUI.AlramPopUp;

    #endregion

    protected override void OnInitialize()
    {
        fieldUI = Instantiate(Resources.Load<FieldUI>("UI/FieldUI"), transform);
        fieldUI.Initialize();
        InitCanvas(fieldUI, CameraManager.Instance.GetCamera(eCameraType.MainCamera).Camera);
        //To do : Addressable ·Î º¯°æ
        _gameUI = Instantiate(Resources.Load<GameUI>("UI/GameUI"), transform);
        _gameUI.Initialize();     
        InitCanvas(_gameUI);
        IsLoad = true;
    }
    void InitCanvas(Component root,Camera targetCamera=null)
    {
        Canvas canvas = root.GetComponent<Canvas>();
        CanvasScaler scaler = root.GetComponent<CanvasScaler>();        

        if(targetCamera!=null)
        {
            canvas.worldCamera = targetCamera;
            canvas.planeDistance = 5;
        }

        if (scaler != null)
        {
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1080,1920);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 1F;
        }
    }
}
