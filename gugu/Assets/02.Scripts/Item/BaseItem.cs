using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : PoolingObject<eItemType>
{
    #region Fields
    [Space]
    [Header("Duration")]
    [SerializeField] protected float durationTime;
    [SerializeField] float elapsedTime;
    #endregion

    #region Item Method
    protected override void ReturnToPool()
    {
        ItemManager.Instance.RegisterToItemPool(worldID, type,objectID);
    }
    void OnItemEffect()
    {
        switch (type)
        {
            case eItemType.Gem:
                //플레이어의 경험치 증진
                break;
            case eItemType.Heart:
                //캐릭터의 체력 회복     
                break;
        }
        Clean(0);
    }
    #endregion

    #region Unity API
    //protected virtual void Update()
    //{
    //    elapsedTime += Time.deltaTime;
    //    if (elapsedTime > durationTime)
    //        Clean(0);
    //}
    protected override void OnClean()
    {
        durationTime = 0;
        elapsedTime = 0;
        base.OnClean();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var actor = collision.GetComponentInParent<Actor>();
        if (actor == null || actor.Type != eActorType.Character || actor.ActorState == eActorState.Death)
            return;

        Debug.Log("아이템 섭취");

        Clean(0);
    }
    #endregion


}
