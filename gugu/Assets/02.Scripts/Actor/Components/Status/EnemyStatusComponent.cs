using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatusComponent : StatusComponent
{
    SOEnemyStatus status;
    public EnemyStatusComponent(Actor owner) : base(owner)
    {
        
    }

    public override float GetStatus(eStatusType type)
    {
        switch (type)
        {

            case eStatusType.MaxHP:
                return status.HP;
            case eStatusType.MoveSpeed:
                return status.MoveSpeed;
            case eStatusType.AttackDamage:
                return status.AttackDamage;
            default:
                return 0;
        }
    }

    public override void SetDefaultStatus()
    {
        status = DataManager.EnemyTable[owner.Index].EnemyStatus;
    }

}
