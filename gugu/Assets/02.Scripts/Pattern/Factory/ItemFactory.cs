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
            _ => 1000,
        };
    }

    protected override string GetResourcePath(eItemType type, int objectID)
    {
        return "Item/Gem_S.prefab";//�̰� ������ ���̺��� ������ �� ..
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
