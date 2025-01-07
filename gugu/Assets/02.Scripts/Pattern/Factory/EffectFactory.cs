using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : Factory<BaseEffect,eEffectType>
{
    public EffectFactory(Transform instanceRoot) : base(instanceRoot)
    {
    }

    protected override void CreateObjectPoolDic()
    {
        objectPool[0] = new Dictionary<int, MemoryPool<BaseEffect>>();
    }

    protected override int GetPoolCapacity(eEffectType type)
    {
        return type switch
        {
            _=>10,
        };
    }

    protected override (string prefabPath, int pathHash) GetResourcePath(eEffectType type, long index)
    {
        string resourcePath = null;
        int pathHash = 0;

        switch (type)
        {

            default:
                {
                    var table = DataManager.EffectTable[index];
                    resourcePath = table.ResourcePath;
                    pathHash = (int)index;
                    break;
                }
        }
        return (resourcePath, pathHash);
    }

    protected override void InitializeObject(BaseEffect obj, eEffectType type, long index, int pathHash)
    {
        obj.Initialize((int)index);
    }

    protected override void ReSetObject(BaseEffect obj,uint worldID, Vector2 position)
    {
        obj.Spawn(worldID, position);
    }

    // Start is called before the first frame update

}
