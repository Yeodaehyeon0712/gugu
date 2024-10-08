using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MainCamera : BaseCamera
{
    CinemachineVirtualCamera virtualCamera;
    public override void Initialize(Vector2 targetResolution, float defaultOrthoSize)
    {
        base.Initialize(targetResolution, defaultOrthoSize);
        CreateVirtualCamera();
    }
    void CreateVirtualCamera()
    {
        var prefab = Resources.Load<CinemachineVirtualCamera>("Camera/VirtualCamera");
        virtualCamera = Instantiate(prefab, CameraManager.Instance.transform);
    }
    public void RegisterFollowTarget(Transform target)
    {
        virtualCamera.m_Follow = target;
    }
}
