using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class SlideToggle : Button
{
    bool _isOn = true;
    [SerializeField] protected Color[] sliderColorArr = new Color[] { Color.white, Color.gray };
    Image _sliderImg;
    Image _handleImg;
    TextMeshProUGUI _stateText;

    Vector2 _handleOffset;
    Vector2 _textOffset;


    Vector2 _targetHandlePosition;
    public SlideToggle Initialize(UnityEngine.Events.UnityAction action)
    {
        _sliderImg = GetComponent<Image>();

        _handleImg = transform.Find("Handle").GetComponent<Image>();
        _handleOffset = _handleImg.rectTransform.anchoredPosition;
        _stateText = transform.Find("Text_State").GetComponent<TextMeshProUGUI>();
        _textOffset = _stateText.rectTransform.anchoredPosition;
        onClick.AddListener(action);
        return this;
    }
    public void IsOn(bool isOn)
    {
        _isOn = isOn;
        _sliderImg.color = _isOn ? sliderColorArr[0] : sliderColorArr[1];
        _targetHandlePosition = _isOn ? _handleOffset : -_handleOffset;

        _stateText.text = _isOn ? "On" : "Off";
        _stateText.rectTransform.anchoredPosition = _isOn ? _textOffset : -_textOffset;
        _stateText.enabled = true;
    }
    protected override void OnEnable()
    {
        base.OnEnable();

        if(Application.isPlaying)
        _handleImg.rectTransform.anchoredPosition = _targetHandlePosition;
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        IsOn(!_isOn);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        _stateText.enabled = false;
        _targetHandlePosition = Vector2.zero;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        IsOn(_isOn);
    }
    void OnUpdateHandle()
    {
        float sqrDistance = (_targetHandlePosition - _handleImg.rectTransform.anchoredPosition).sqrMagnitude;
        if ((sqrDistance < float.Epsilon && sqrDistance > 0) || (sqrDistance > -float.Epsilon && sqrDistance < 0))
        {
            _handleImg.rectTransform.anchoredPosition = _targetHandlePosition;
        }
        else
        {
            _handleImg.rectTransform.anchoredPosition = Vector2.Lerp(_handleImg.rectTransform.anchoredPosition, _targetHandlePosition, 0.5f);
        }
    }
    void LateUpdate()
    {
        if (!Application.isPlaying)
            return;

        OnUpdateHandle();
    }
}
