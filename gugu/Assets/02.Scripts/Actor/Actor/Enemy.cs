using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor,IRepositionObject
{

    #region Fields
    //Reposition Fields
    [SerializeField] protected Transform repositionArea;
    [SerializeField] LayerMask areaLayer;
    #endregion

    #region EnemyMethod
    public override void Initialize(eActorType type, int objectID)
    {
        base.Initialize(type,objectID);
        SetRepositionArea(ActorManager.Instance.SpawnArea.transform);
    }
    #endregion
    protected override void InitializeComponent()
    {
        //Default Component
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(DataManager.EnemyTable[objectID].AnimatorPath));
        statusComponent = new EnemyStatusComponent(this);
        controllerComponent = new EnemyControllerComponent(this);

        //Enemy Component
    }

    #region Reposition Method
    public void SetRepositionArea(Transform repositionArea)
    {
        this.repositionArea = repositionArea;
        areaLayer = LayerMask.GetMask(LayerMask.LayerToName(repositionArea.gameObject.layer));
    }
    public void Reposition()
    {
        var position = ActorManager.Instance.GetRandomPosition();
        controllerComponent.TeleportActor(position);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((areaLayer & (1 << collision.gameObject.layer)) == 0) || state == eActorState.Death)  return;

        Reposition();
    }
    #endregion
}
