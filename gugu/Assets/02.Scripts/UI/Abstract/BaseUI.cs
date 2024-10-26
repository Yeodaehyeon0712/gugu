using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUI : MonoBehaviour,IObserver<eLanguage>
{
    public virtual BaseUI Initialize()
    {
        InitReference();
        LocalizingManager.Instance.AddObserver(this);
        Refresh();
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
    public void OnNotify(eLanguage value)
    {
        OnRefresh();
    }
    protected abstract void OnRefresh();

}
