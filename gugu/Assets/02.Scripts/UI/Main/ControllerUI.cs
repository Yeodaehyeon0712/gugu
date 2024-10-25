using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ControllerUI : BaseUI,IPointerDownHandler, IPointerUpHandler, IDragHandler, ISubject<Vector2>
{
    #region Fields
    //ISubject Fields
    List<IObserver<Vector2>> observers = new List<IObserver<Vector2>>();

    //UI Fields
    RectTransform controlAreaRect;
    RectTransform controllerRect;
    RectTransform joyStickRect;
    CanvasGroup canvasGroup;
    
    //Controller Fields
    [SerializeField]
    float movementRange = 50;
    Vector2 controllerStartPos;
    Vector3 joyStickStartPos;
    Vector2 pointerDownPos;
    #endregion

    #region InitMethod
    protected override void InitReference()
    {
        controlAreaRect = transform as RectTransform;
        controllerRect = transform.Find("Img_Controller").GetComponent<RectTransform>();
        joyStickRect = transform.Find("Img_Controller/Img_JoyStick").GetComponent<RectTransform>();
        canvasGroup = controllerRect.GetComponent<CanvasGroup>();

        controllerStartPos = controllerRect.anchoredPosition;
        joyStickStartPos = joyStickRect.anchoredPosition;
        SetControllerAlpha(RuntimePreference.Preference.JoyStick ? 1 : 0);
    }

    protected override void OnRefresh()
    {
        
    }
    #endregion

    #region IPointer Method
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));
        MoveControllerToPosition(eventData.position);
        // To Do : 만약 UI 카메라를 따로 들이고 , Canvas Mode가 Overray가 아니라면 null이 아닌 카메라 전달 .
        RectTransformUtility.ScreenPointToLocalPointInRectangle(controllerRect, eventData.position, null, out pointerDownPos);
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(controllerRect, eventData.position, null, out var position);
        var delta = position - pointerDownPos;

        delta = Vector2.ClampMagnitude(delta, movementRange);
        joyStickRect.anchoredPosition = joyStickStartPos + (Vector3)delta;

        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        Notify(newPos);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joyStickRect.anchoredPosition = joyStickStartPos;
        controllerRect.anchoredPosition = controllerStartPos;
        Notify(Vector2.zero);
    }


    #endregion

    #region Controller Method
    private void MoveControllerToPosition(Vector2 position)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(controlAreaRect, position, null, out Vector2 localPoint))
            controllerRect.anchoredPosition = localPoint;
    }
    public void SetControllerAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
    #endregion

    #region ISubject Method
    public void AddObserver(IObserver<Vector2> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }
    public void RemoveObserver(IObserver<Vector2> observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
    public void Notify(Vector2 value)
    {
        if (observers.Count < 1)
            return;

        foreach (var observer in observers)
            observer.OnNotify(value);
        //이건 목록 복사를 고민해보자 .
        //var observersCopy = new List<IObserver<eLanguage>>(observers);
        //foreach (var observer in handlersCopy)
        //{
        //    observer.OnNotify(value);
        //}
    }
    #endregion
}
