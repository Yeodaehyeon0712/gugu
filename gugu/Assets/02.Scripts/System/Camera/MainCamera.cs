using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
public class MainCamera : BaseCamera
{
    #region Fields
    CinemachineVirtualCamera virtualCamera;
    PixelPerfectCamera pixelPerfectCamera;
    int minimumPPU;
    #endregion

    #region Init Method
    public override void Initialize(Vector2 targetResolution)
    {
        base.Initialize(targetResolution);
        CreateVirtualCamera();
        SetMinimumPPU();
    }
    void CreateVirtualCamera()
    {
        var prefab = Resources.Load<CinemachineVirtualCamera>("Camera/VirtualCamera");
        virtualCamera = Instantiate(prefab, CameraManager.Instance.transform);
    }

    public void SetMinimumPPU()
    {
        pixelPerfectCamera = GetComponent<PixelPerfectCamera>();
        var scaleFactor = Screen.height / GameConst.defaultResolution.y;
        minimumPPU = Mathf.RoundToInt(GameConst.defaultMinimumPPU * scaleFactor);
        pixelPerfectCamera.assetsPPU = minimumPPU;
    }
    #endregion

    #region Follow Method
    public void RegisterFollowTarget(Transform target)
    {
        virtualCamera.m_Follow = target;
    }
    #endregion

    #region Zoom Method
    public void Zoom(float rate, float duration)
    {
        var targetPPU = Mathf.RoundToInt(currentPPU * rate);
        DOTween.To(() => pixelPerfectCamera.assetsPPU, x => pixelPerfectCamera.assetsPPU = x, Mathf.RoundToInt(currentPPU * rate), duration).SetEase(Ease.InOutQuad).SetUpdate(true);
    }
    #endregion
}
