using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory : Factory<Actor,eActorType>
{
    #region Abstract Method
    public ActorFactory(Transform instanceRoot) : base(instanceRoot)
    {
        
    }

    protected override void CreateObjectPoolDic()
    {
        for (eActorType i = eActorType.None; i < eActorType.End; ++i)
            objectPool[i] = new Dictionary<int, MemoryPool<Actor>>();
    }

    protected override int GetPoolCapacity(eActorType type)
    {
        return type switch
        {
            eActorType.Character => 5,
            eActorType.Enemy=>50,
            _ => 0,
        };
    }

    protected override (string prefabPath, int objectID) GetResourcePath(eActorType type, long index)
    {
        string resourcePath = null;
        int pathHash = 0;

        switch (type)
        {
            case eActorType.Character:
                {
                    var table = DataManager.CharacterTable[index];
                    resourcePath = table.ResourcePath;
                    pathHash = table.PathHash;
                    break;
                }
            case eActorType.Enemy:
                {
                    var table = DataManager.EnemyTable[index];
                    resourcePath = table.ResourcePath;
                    pathHash = table.PathHash;
                    break;
                }
        }
        return (resourcePath,pathHash);
    }

    protected override void InitializeObject(Actor obj, eActorType type, long index, int objectID)
    {
        obj.Initialize(type, index, objectID);
    }

    protected override void ReSetObject(Actor obj, uint worldID, Vector2 position)
    {
        obj.ResetComponent();
        obj.Spawn(worldID,position);
    }
    #endregion  
}
