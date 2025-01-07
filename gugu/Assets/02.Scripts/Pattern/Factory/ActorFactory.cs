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

    protected override (string prefabPath, int pathHash) GetResourcePath(eActorType type, long index)
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

    protected override void InitializeObject(Actor obj, eActorType type, long index, int pathHash)
    {
        obj.Initialize(type, index, pathHash);
    }

    protected override void ReSetObject(Actor obj, uint worldID, Vector2 position)
    {
        RefreshActorSkin(obj, obj.ActorType, obj.Index);
        RefreshActorStat(obj, obj.ActorType, obj.Index);
        obj.Spawn(worldID,position);
    }
    #endregion

    #region Actor Factory Method
    void RefreshActorSkin(Actor actor, eActorType type, long index)
    {
        RuntimeAnimatorController animator = type switch
        {
            eActorType.Character => AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath),
            eActorType.Enemy => AddressableSystem.GetAnimator(DataManager.EnemyTable[index].AnimatorPath),
            _ => null
        };
        actor.Skin.SetSkin(animator);
    }
    void RefreshActorStat(Actor actor, eActorType type, long index)
    {
        switch (type)
        {
            case eActorType.Character:
                {
                    var target = actor as Character;
                    target.Stat.SetStat(DataManager.CharacterTable[index]);
                    break;
                }
            case eActorType.Enemy:
                {
                    var target = actor as Enemy;
                    target.Stat = DataManager.EnemyStats[index];
                    break;
                }
        }
    }
    #endregion
}
