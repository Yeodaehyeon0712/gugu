using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : Factory<BaseEffect,eEffectType>
{
    public EffectFactory(Transform instanceRoot) : base(instanceRoot)
    {
    }


    protected override int GetPoolCapacity(eEffectType type)
    {
        return type switch
        {
            _ => 10,
        };
    }

    protected override string  GetResourcePath(eEffectType type, int objectID)
    {
        return DataManager.EffectTable[objectID].ResourcePath;
    }

    protected override void InitializeObject(BaseEffect obj, eEffectType type, int objectID)
    {
        obj.Initialize(type,objectID);
    }

    protected override void SpawnObject(BaseEffect obj,uint worldID, Vector2 position)
    {
        obj.Spawn(worldID, position);
    }

}
