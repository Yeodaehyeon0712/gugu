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
    //��ȯ�� ������� �ʴ� ��
    void TestCase1()
    {
        a.Initialize(1);
        //a.Enable(1,a.transform.position,0);
    }
    //��ȯ�� ������� ��
    void TestCase2()
    {
        a.Initialize(1);
        //a.Enable(1, a.transform.position, 3);
    }
    //��ȯ�� ��ǥ�� ���� ���󰡸� �浹�ϴ� �� .
    void TestCase3()
    {
        a.Initialize(1);
        //a.Enable(1, a.transform.position, 0);
        //    a.SetVelocity(target)
        //    .SetOverlapEvent(eActorType.Character)
        //    .SetPostEffect(ePostEffectType.KnockBack, 3f);
    }
    //��ȯ�� ��ǥ�� ���� ���󰡸� �浹���� ȿ������ ����
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