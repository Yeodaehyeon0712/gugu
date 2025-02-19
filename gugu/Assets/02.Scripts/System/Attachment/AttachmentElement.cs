using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AttachmentElement : MonoBehaviour
{
    public eAttachmentTarget AttachmentTarget;
    public Transform Transform;
    private void Awake()
    {
        Transform = transform;
    }
}
