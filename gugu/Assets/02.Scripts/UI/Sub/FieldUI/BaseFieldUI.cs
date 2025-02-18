using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFieldUI : MonoBehaviour
{
    protected MemoryPool<BaseFieldUI>.del_Register del_Register;
    public virtual BaseFieldUI Init(MemoryPool<BaseFieldUI>.del_Register register)
    {
        del_Register = register;
        return this;
    }
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }
    public virtual void Disable()
    {
        del_Register(this);
        gameObject.SetActive(false);
    }
}
