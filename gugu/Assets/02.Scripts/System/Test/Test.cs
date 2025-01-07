using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using Cinemachine;

public class Test : MonoBehaviour
{
    public BaseEffect a;
    public Transform target;

    void Start()
    {
        TestCase3();
    }
    //소환후 사라지지 않는 것
    void TestCase1()
    {
        a.Initialize(1);
        //a.Enable(1,a.transform.position,0);
    }
    //소환후 사라지는 것
    void TestCase2()
    {
        a.Initialize(1);
        //a.Enable(1, a.transform.position, 3);
    }
    //소환후 목표를 향해 날라가며 충돌하는 것 .
    void TestCase3()
    {
        a.Initialize(1);
        //a.Enable(1, a.transform.position, 0);
        //    a.SetVelocity(target)
        //    .SetOverlapEvent(eActorType.Character)
        //    .SetPostEffect(ePostEffectType.KnockBack, 3f);
    }
    //소환후 목표를 향해 날라가며 충돌이후 효과까지 포함
    void TestCase4()
    {

    }

    void AdjustCameraForResolution()
    {
        var a = Resources.Load<Camera>("Camera/MainCamera");
        Instantiate(a);
        var b = Resources.Load<CinemachineVirtualCamera>("Camera/VirtualCamera");
        Instantiate(b);
    }
}