using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class USafeArea : MonoBehaviour,ISubject<(Vector2,Vector2)>
{
    #region Fields
    ScreenOrientation lastOrientation;
    RectTransform safeAreaTransform;
    List<IObserver<(Vector2, Vector2)>> observers=new List<IObserver<(Vector2, Vector2)>>();
    #endregion

    #region SafeArea Method
    public void Initialize()
    {
        safeAreaTransform = transform as RectTransform;
        ApplySafeArea();
    }
    private void Update()
    {
        if (Screen.orientation == lastOrientation||observers.Count<1) return;
            ApplySafeArea();
    }   
    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;
        Vector2 anchorMin = safeArea.position;
        Vector2 anchorMax = safeArea.position + safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        safeAreaTransform.anchorMin = anchorMin;
        safeAreaTransform.anchorMax = anchorMax;

        lastOrientation = Screen.orientation;
        Notify((anchorMin, anchorMax));
    }
    #endregion

    #region ISubject Method
    public void AddObserver(IObserver<(Vector2, Vector2)> observer)
    {
        if (observers.Contains(observer)==false)
            observers.Add(observer);
    }
    public void RemoveObserver(IObserver<(Vector2, Vector2)> observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
    public void Notify((Vector2, Vector2) value)
    {
        if (observers.Count < 1) return;

        foreach (var observer in observers)
            observer.OnNotify(value);
    }
    #endregion
}
