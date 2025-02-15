using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IRepositionObject
{
    void SetRepositionArea(Transform target);
    public void Reposition();
}

public abstract class RepositionObject : MonoBehaviour,IRepositionObject
{
    #region Fields
    [SerializeField]protected Transform repositionArea;
    [SerializeField] LayerMask areaLayer;
    #endregion

    #region Reposition Method
    public virtual void Initialize(Transform repositionArea)
    {
        SetRepositionArea(repositionArea);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((areaLayer & (1 << collision.gameObject.layer)) == 0) return;
        Reposition();
    }
    public void SetRepositionArea(Transform repositionArea)
    {
        this.repositionArea =repositionArea;
        areaLayer = LayerMask.GetMask(LayerMask.LayerToName(this.repositionArea.gameObject.layer));
    }
    public void Reposition()
    {
        OnReposition();
    }
    protected abstract void OnReposition();
    #endregion
}

