using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : PoolingObject<eItemType>
{
    #region Fields
    Data.ItemData itemData;
    bool isActive;
    float durationTime;
    float elapsedTime;
    #endregion

    #region Pooling Method
    public override void Initialize(eItemType type, int objectID)
    {
        base.Initialize(type, objectID);
        itemData = DataManager.ItemTable[objectID];
    }
    public override void Spawn(uint worldID, Vector2 position)
    {
        base.Spawn(worldID, position);
        isActive = true;
        durationTime = itemData.DurationTime;
    }
    protected override void ReturnToPool()
    {
        ItemManager.Instance.RegisterToItemPool(worldID, type, objectID);
    }
    #endregion

    #region Item Method
    void OnItemEffect()
    {
        switch (type)
        {
            case eItemType.Gem:
                Player.GetExp(itemData.GetValue());
                break;
            case eItemType.Heart:
                break;
        }
        Clean(0);
    }
    #endregion

    #region Unity API
    protected virtual void Update()
    {
        if (isActive == false) return;

        elapsedTime += TimeManager.DeltaTime;
        if (elapsedTime > durationTime)
            Clean(0);
    }
    protected override void OnClean()
    {
        isActive = false;
        durationTime = 0;
        elapsedTime = 0;
        base.OnClean();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var actor = collision.GetComponentInParent<Actor>();
        if (actor == null || actor.Type != eActorType.Character || actor.ActorState == eActorState.Death)
            return;

        OnItemEffect();
    }
    #endregion
}
