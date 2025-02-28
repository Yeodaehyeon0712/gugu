using System.Collections;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : TSingletonMono<ItemManager>
{
    ItemFactory itemFactory;
    protected override void OnInitialize()
    {
        IsLoad = true;
    }
    #region Spawn Method
    public async UniTask<BaseItem> SpawnItem(eItemType type, int objectID, Vector3 position, Transform parent = null) => await itemFactory.SpawnObjectAsync(type, objectID, position, parent);
    public void RegisterToItemPool(uint worldID, eItemType type, int pathHash) => itemFactory.RegisterToObjectPool(worldID, type, pathHash);
    public Dictionary<uint, BaseItem> GetSpawnedItem => itemFactory.GetSpawnedObjects;
    #endregion
}
