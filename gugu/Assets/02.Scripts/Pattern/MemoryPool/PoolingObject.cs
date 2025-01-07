using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObject : MonoBehaviour
{
    protected uint worldID;
    public uint WorldID=>worldID;
    protected int objectID;
    public int PathHashCode => objectID;
    bool isClean;
    public void SetObjectID(int objectID)
    {
        this.objectID = objectID;
    }
    public virtual void Spawn(uint worldID,Vector2 position)
    {
        this.worldID = worldID;
        isClean = false;
        transform.position = position;
        gameObject.SetActive(true);
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
        gameObject.SetActive(false);
        ReturnToPool();
    }
    protected abstract void ReturnToPool();
}


