using System.Collections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : TSingletonMono<ItemManager>
{
    ItemFactory itemFactory;
    protected override void OnInitialize()
    {
        itemFactory = new ItemFactory(transform);
        IsLoad = true;
    }
    #region Spawn Method
    public async UniTask<BaseItem> SpawnItem(int objectID, Vector3 position, Transform parent = null) => await itemFactory.SpawnObjectAsync(DataManager.ItemTable[objectID].ItemType, objectID, position, parent);
    public void RegisterToItemPool(uint worldID, eItemType type, int objectID) => itemFactory.RegisterToObjectPool(worldID, type, objectID);
    public Dictionary<uint, BaseItem> GetSpawnedItem => itemFactory.GetSpawnedObjects;
    #endregion
}
