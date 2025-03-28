using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObject<TType> : MonoBehaviour where TType : System.Enum
{
    #region Fields
    public TType Type => type;
    protected TType type;
    public int ObjectID => objectID;
    protected int objectID;
    public uint WorldID => worldID;
    protected uint worldID;
    public bool ResetParent => resetParent;
    protected bool resetParent;

    bool isClean;
    #endregion

    #region Pooling Method
    public virtual void Initialize(TType type, int objectID)
    {
        this.type = type;
        this.objectID = objectID;
    }
    public virtual void Spawn(uint worldID, Vector2 position)
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
        ReturnToPool();
        isClean = true;
        resetParent = false;
        gameObject.SetActive(false);
    }
    protected abstract void ReturnToPool();
    #endregion
}


