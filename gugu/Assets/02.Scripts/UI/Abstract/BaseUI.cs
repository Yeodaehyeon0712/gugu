using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public virtual BaseUI Initialize()
    {
        InitReference();
        Disable();
        return this;
    }
    protected abstract void InitReference();
    public virtual void Enable()
    {
        gameObject.SetActive(true);
    }
    public virtual void Disable()
    {
        gameObject.SetActive(false);
    }
    public virtual void Refresh()
    {
        OnRefresh();
    }
    protected abstract void OnRefresh();

}
