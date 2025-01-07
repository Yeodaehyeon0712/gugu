using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolingObject : MonoBehaviour
{
    protected uint worldID;
    public uint WorldID=>worldID;
    protected int pathHashCode;
    public int PathHashCode => pathHashCode;
    bool isClean;
    public void SetPathHashCode(int pathHash)
    {
        this.pathHashCode = pathHash;
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


