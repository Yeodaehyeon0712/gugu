using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    public SkillComponent Skill => skillComponent;
    [SerializeField] protected SkillComponent skillComponent;

    public Data.CharacterData characterData;
    int collisionCount = 0;
    float recoveryTime;
    #endregion

    #region Unity API
    protected override void Update()
    {
        base.Update();
        Recovery();
    }
    #endregion

    #region Collision Method

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false) return;

        CheckCollisionState(isCollisonEnter: true);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false) return;

        var enemy = collision.gameObject.GetComponentInParent<Enemy>();
        var damage = CalculateDamage(enemy);

        Hit(TimeManager.DeltaTime * damage);
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (CheckTargetLayer(collision.gameObject.layer)==false) return;

        CheckCollisionState(isCollisonEnter: false);
    }
    void CheckCollisionState(bool isCollisonEnter)
    {
        collisionCount += isCollisonEnter ? 1 : -1;

        if (collisionCount == (isCollisonEnter ? 1 : 0))
            Skin.SetSkinColor(isCollisonEnter ? Color.red : Color.white);
    }
    #endregion

    #region Actor Method
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
    public override void Death(float time = 2.5F)
    {
        base.Death(time);
        Timer.SetTimer(time, true, () => StageManager.Instance.StopStage(skipResult: false));
    }
    #endregion

    #region Character Method
    void Recovery()
    {
        recoveryTime += TimeManager.DeltaTime;
        if (recoveryTime >= 1f)
        {
            float recoveryVal = currentHP + Status.GetStatus(eStatusType.Recovery);
            currentHP = Mathf.Min(recoveryVal, Status.GetStatus(eStatusType.MaxHP));
            recoveryTime = 0f;
        }
    }
    float CalculateDamage(Enemy enemy)
    {
        float damage = enemy.Status.GetStatus(eStatusType.Might);
        float armor = Status.GetStatus(eStatusType.Armor);
        return Mathf.Max(damage - armor, 0);
    }
    #endregion
}
