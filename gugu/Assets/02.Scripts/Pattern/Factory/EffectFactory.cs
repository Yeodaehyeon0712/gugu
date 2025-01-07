using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFactory : Factory<BaseEffect>
{
    public EffectFactory(Transform instanceRoot) : base(instanceRoot)
    {
    }

    protected override void CreateObjectPoolDic()
    {
        objectPool[0] = new Dictionary<int, MemoryPool<BaseEffect>>();
    }

    protected override int GetPoolCapacity(uint type)
    {
        return 30;
    }

    protected override (string prefabPath, int pathHash) GetResourcePath(uint type, long index)
    {
        string resourcePath = null;
        int pathHash = (int)index;

        var table = DataManager.EffectTable[(int)index];
        resourcePath = table.ResourcePath;

        return (resourcePath, pathHash);
    }

    protected override void InitializeObject(BaseEffect obj, uint type, long index, int pathHash)
    {
        obj.Initialize((int)index);
    }

    protected override void ReSetObject(BaseEffect obj,uint worldID, Vector2 position)
    {
        obj.Spawn(worldID, position);
    }

    // Start is called before the first frame update

}
