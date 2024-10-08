using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : TSingletonMono<CameraManager>
{
    Dictionary<eCameraType, BaseCamera> cameraDic;
    Vector2 targetResolution = new Vector2(1080, 1920);
    public float defaultOrthoSize = 15.0f;
    protected override void OnInitialize()
    {
        cameraDic = new Dictionary<eCameraType,BaseCamera>();
        cameraDic.Add(eCameraType.MainCamera,CreateCamera(eCameraType.MainCamera));
        IsLoad = true;
    }
    BaseCamera CreateCamera(eCameraType type)
    {
        var prefab = Resources.Load<BaseCamera>("Camera/" + type.ToString());
        BaseCamera cam = Instantiate(prefab, transform);
        cam.Initialize(targetResolution, defaultOrthoSize);
        return cam;
    }  
    public void RegisterFollowTarget(Transform target)
    {
        var mainCam = cameraDic[eCameraType.MainCamera] as MainCamera;
        mainCam.RegisterFollowTarget(target);
    }
}
public enum eCameraType
{
    MainCamera,
    VirtualCamera,
}
