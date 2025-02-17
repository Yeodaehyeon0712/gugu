using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObject : MonoBehaviour
{
    #region Fields
    public uint WorldID=>worldID;
    protected uint worldID;
    public int ObjectID { get=>objectID; set=>objectID=value; }
    protected int objectID;
    public bool ResetParent => resetParent;
    protected bool resetParent;

    bool isClean;
    #endregion

    #region Pooling Method
    protected void Initialize(int objectID)
    {
        this.objectID = objectID;
    }
    public virtual void Spawn(uint worldID,Vector2 position)
    {
        this.worldID = worldID;
        transform.position = position;
        isClean = false;
        gameObject.SetActive(true);
    }
    public void SetParent(Transform parent)
    {
        resetParent = true;
        transform.SetParent(parent);
    }
    public void Clean(float time)
    {
        if (time == 0f) OnClean();
        else Timer.SetTimer(time, true, OnClean, CleanCondition);
    }
    bool CleanCondition()
    {
        return isClean==false;
    }
    protected virtual void OnClean()
    {
        isClean = true;
        resetParent = false;
        gameObject.SetActive(false);
        ReturnToPool();
    }
    protected abstract void ReturnToPool();
    #endregion
}


