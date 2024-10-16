using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControllerComponent :ControllerComponent
{
    #region Component Method
    public EnemyControllerComponent(Actor owner) : base(owner)
    {

    }
    #endregion

    protected override void MoveActor(float fixedDeltaTime)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
