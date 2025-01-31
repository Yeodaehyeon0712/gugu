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
        for (eEffectType i = eEffectType.None+1; i < eEffectType.End; ++i)
            objectPool[i] = new Dictionary<int, MemoryPool<BaseEffect>>();
    }

    protected override int GetPoolCapacity(eEffectType type)
    {
        return type switch
        {
            _ => 10,
        };
    }

    protected override (string prefabPath, int objectID) GetResourcePath(eEffectType type, long index)
    {
        string resourcePath = null;
        int objectID = 0;

        switch (type)
        {
            default:
                {
                    var table = DataManager.EffectTable[index];
                    resourcePath = table.ResourcePath;
                    objectID = (int)index;
                    break;
                }
        }
        return (resourcePath, objectID);
    }

    protected override void InitializeObject(BaseEffect obj, eEffectType type, long index, int objectID)
    {
        obj.Initialize((int)index);
    }

    protected override void SpawnObject(BaseEffect obj,uint worldID, Vector2 position)
    {
        obj.Spawn(worldID, position);
    }

}
