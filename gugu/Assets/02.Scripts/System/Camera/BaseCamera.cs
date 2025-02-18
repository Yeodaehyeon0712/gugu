using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCamera : MonoBehaviour
{
    #region Fields
    public Camera Camera => _camera;
    Camera _camera;
    #endregion
    public virtual void Initialize(Vector2 targetResolution)
    {
        _camera = GetComponent<Camera>();
    }
}
