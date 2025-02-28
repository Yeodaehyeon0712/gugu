using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorFactory : Factory<Actor,eActorType>
{
    #region Abstract Method
    public ActorFactory(Transform instanceRoot) : base(instanceRoot)
    {
        
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

    protected override string  GetResourcePath(eActorType type,int objectID)
    {
        return type switch
        {
            eActorType.Character => DataManager.CharacterTable[objectID].ResourcePath,
            eActorType.Enemy=>DataManager.EnemyTable[objectID].ResourcePath,
            _=>"",
        };
    }

    protected override void InitializeObject(Actor obj, eActorType type,int objectID)
    {
        obj.Initialize(type, objectID);
    }

    protected override void SpawnObject(Actor obj, uint worldID, Vector2 position)
    {
        obj.Spawn(worldID,position);
    }
    #endregion  
}
