using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    public SkillComponent Skill => skillComponent;
    [SerializeField] protected SkillComponent skillComponent;
    public Data.CharacterData characterData;
    public bool hasDamagedThisFrame = false;
    int collisionCount = 0;
    #endregion

    #region Character Method
    public override void Initialize(eActorType type,int objectID)
    {
        base.Initialize(type,objectID);
        targetLayer = 1<< LayerMask.NameToLayer("Enemy");      
    }
    protected override void InitializeComponent()
    {
        //Default Component
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(DataManager.CharacterTable[objectID].AnimatorPath));
        statusComponent = new CharacterStatusComponent(this);
        controllerComponent = new CharacterControllerComponent(this);

        //Character Component
        skillComponent = new SkillComponent(this);
        characterData = DataManager.CharacterTable[objectID];
    }
    void CheckCollisionState(bool isCollisonEnter)
    {
        collisionCount += isCollisonEnter ? 1 : -1;

        if (collisionCount == (isCollisonEnter ? 1 : 0))
            Skin.SetSkinColor(isCollisonEnter ? Color.red : Color.white);
    }
    #endregion

    #region Unity API
    //private void LateUpdate()
    //{
    //    hasDamagedThisFrame = false;
    //}
    #endregion

    #region Collision Method

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false) return;

        CheckCollisionState(isCollisonEnter: true);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false /*|| hasDamagedThisFrame*/) return;

        var enemy = collision.gameObject.GetComponentInParent<Enemy>();
        var damage = enemy.Status.GetStatus(eStatusType.Might);
        //To Do : 이 부분 수정


        Hit(TimeManager.DeltaTime * damage);
        //hasDamagedThisFrame = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false) return;

        CheckCollisionState(isCollisonEnter: false);
    }
    #endregion

    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        Timer.SetTimer(time,true,()=> StageManager.Instance.StopStage(skipResult: false));
    }
}
