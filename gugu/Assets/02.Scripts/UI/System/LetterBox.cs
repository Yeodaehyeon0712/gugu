using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterBox : MonoBehaviour,IObserver<(Vector2,Vector2)>
{
    #region Fields
    RectTransform _panel;
    [SerializeField]  LetterBoxDirection _direction = LetterBoxDirection.Top;
    #endregion

    #region Letter Box Method
    public void Initialize(SafeArea safeArea)
    {
        _panel = (RectTransform)transform;
        safeArea.AddObserver(this);
    }
    void UpdateLetterBox((Vector2 min, Vector2 max) value)
    {
        if (_direction == LetterBoxDirection.Top)
            _panel.anchorMin = new Vector2(0, value.max.y);
        else
            _panel.anchorMax = new Vector2(1, value.min.y);

        _panel.sizeDelta = Vector2.zero;
        _panel.anchoredPosition = Vector2.zero;
    }
    #endregion

    #region IObserver Method
    public void OnNotify((Vector2, Vector2) value)
    {
        UpdateLetterBox(value);
    }
    #endregion
}
