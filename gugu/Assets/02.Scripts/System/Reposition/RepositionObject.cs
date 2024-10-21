using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepositionObject : MonoBehaviour
{
    #region Fields
    [SerializeField]protected Transform spawnArea;
    [SerializeField] LayerMask targetLayers;
    #endregion

    #region Reposition Method
    public virtual void Initialize(Transform spawnArea)
    {
        this.spawnArea = spawnArea;
        targetLayers = LayerMask.GetMask(LayerMask.LayerToName(spawnArea.gameObject.layer));
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((targetLayers & (1 << collision.gameObject.layer)) == 0) return;

        Reposition();
    }
    protected abstract void Reposition();
    #endregion
}
