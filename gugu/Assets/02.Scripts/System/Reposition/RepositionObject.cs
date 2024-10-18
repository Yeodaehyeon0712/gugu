using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RepositionObject : MonoBehaviour
{
    #region Fields
    //���� ���� ���� - �÷��̾��� ��ġ
    [SerializeField]protected Transform target;
    [SerializeField]LayerMask targetLayers=LayerMask.GetMask("RepositionArea");
    #endregion

    #region Reposition Method
    public void Initialize()
    {

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        int collisionLayerMask = 1 << collision.gameObject.layer;
        if ((targetLayers.value & collisionLayerMask) == 0) return; 

        Reposition();
    }
    protected abstract void Reposition();
    #endregion
}
