using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class SwitchButton : Button
{
    public event System.Action OnPointerDownEvent;
    public event System.Action OnPointerUpEvent;
    public event System.Action OnPointerExitEvent;
    [SerializeField] protected Sprite _onImage;
    [SerializeField] protected Sprite _offImage;
    [SerializeField] protected Image _targetGraphic;
    bool _isOn;
    public bool IsOn => _isOn;
    [SerializeField] protected int _onClickSoundKey;
    [SerializeField] protected int _offClickSoundKey;

    #region Editor
    public Image TargetGraphic {
        get => _targetGraphic;
        set => _targetGraphic = value;
    }
    #endregion
    
    public void SetImage(bool isOn)
    {
        if (isOn != _isOn)
            _isOn = isOn;

        _targetGraphic.sprite = _isOn ? _onImage : _offImage;
    }
    
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        if (OnPointerDownEvent != null)
            OnPointerDownEvent();

        if (_isOn)
        {
            //if (_onClickSoundKey != 0)
            //    SoundManager.Instance.PlaySFXMusic(_onClickSoundKey);
        }
        else
        {
            //if (_offClickSoundKey != 0)
            //    SoundManager.Instance.PlaySFXMusic(_offClickSoundKey);
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        if (OnPointerUpEvent != null)
            OnPointerUpEvent();
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);

        if (OnPointerExitEvent != null)
            OnPointerExitEvent();
    }
}
