using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    #region Fields
    Camera _camera;

    #endregion
    public virtual void Initialize(Vector2 targetResolution, float defaultOrthoSize)
    {
        _camera = GetComponent<Camera>();
        //SetOrthoByResolution(targetResolution, defaultOrthoSize);
    }
    void SetOrthoByResolution(Vector2 targetResolution , float defaultOrthoSize)
    {
        float targetRate = targetResolution.x / targetResolution.y;
        float screenRate = Screen.safeArea.width / Screen.safeArea.height;
        var scaleFactor = targetRate / screenRate;
        _camera.orthographicSize = defaultOrthoSize * scaleFactor;
    }
}
