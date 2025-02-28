using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : Factory<BaseItem, eItemType>
{
    public ItemFactory(Transform instanceRoot) : base(instanceRoot)
    {

    }
    protected override int GetPoolCapacity(eItemType type)
    {
        return type switch
        {
            _ => 10,
        };
    }

    protected override string GetResourcePath(eItemType type, int objectID)
    {
        return "";//이건 아이템 테이블을 만들어야 해 ..
    }

    protected override void InitializeObject(BaseItem obj, eItemType type, int objectID)
    {
        obj.Initialize(type, objectID);
    }

    protected override void SpawnObject(BaseItem obj, uint worldID, Vector2 position)
    {
        obj.Spawn(worldID, position);
    }
}
