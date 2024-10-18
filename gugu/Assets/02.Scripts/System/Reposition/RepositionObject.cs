using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepositionObject : MonoBehaviour
{
    #region Fields
    [SerializeField]protected Transform repositionArea;
    [SerializeField] LayerMask targetLayers;
    #endregion

    #region Reposition Method
    public virtual void Initialize(Transform target)
    {
        this.repositionArea = target;
        targetLayers = LayerMask.GetMask(LayerMask.LayerToName(target.gameObject.layer));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) == 0) return;

        Reposition();
    }
    protected abstract void Reposition();
    #endregion
}
