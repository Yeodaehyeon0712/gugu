using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatComponent : BaseComponent
{
    #region Fields
    #endregion

    #region Component Method
    public StatComponent(Actor owner) : base(owner, eComponent.StatComponent,useUpdate:false)
    {

    }
    #endregion
    public void SetStat(long index)
    {

    }
    //각종 버프 , 아이템으로 인한 수치의 최종값을 전달 .
    //버프
    //스킬을 가지고만 있자 ..

    //뱀서라이크의 능력치
    //
}
