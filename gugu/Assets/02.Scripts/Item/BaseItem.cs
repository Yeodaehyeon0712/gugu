using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : PoolingObject<eItemType>
{
    protected override void ReturnToPool()
    {
        throw new System.NotImplementedException();
    }

}
