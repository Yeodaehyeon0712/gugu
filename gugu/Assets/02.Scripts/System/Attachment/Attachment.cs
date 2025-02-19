using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[DisallowMultipleComponent]
public class Attachment : MonoBehaviour
{
    Dictionary<eAttachmentTarget, AttachmentElement> _attachmentDictionary;
    public void Initialize()
    {
        _attachmentDictionary = new Dictionary<eAttachmentTarget, AttachmentElement>();
        AttachmentElement[] attachmentElement = GetComponentsInChildren<AttachmentElement>();
        if (attachmentElement != null)
            for (int i = 0; i < attachmentElement.Length; ++i)
                _attachmentDictionary.Add(attachmentElement[i].AttachmentTarget, attachmentElement[i]);
    }
    public AttachmentElement GetAttachmentElement(eAttachmentTarget target)
    {
        if (_attachmentDictionary.ContainsKey(target))
            return _attachmentDictionary[target];

        return null;
    }   
}
