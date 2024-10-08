using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class Test : MonoBehaviour
{
    public int targetWidth = 1080;
    public int targetHeight = 1920;
    public float idealOrthoSize = 15.0f;

    void Start()
    {
        AdjustCameraForResolution();
    }

    void AdjustCameraForResolution()
    {
        var a = Resources.Load<Camera>("Camera/MainCamera");
        Instantiate(a);
        var b = Resources.Load<CinemachineVirtualCamera>("Camera/VirtualCamera");
        Instantiate(b);
    }
}