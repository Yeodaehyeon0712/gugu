using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionArea : MonoBehaviour
{
    //계속 플레이어를 추적할것이다 .
    Transform target;
    BoxCollider2D boxCollider;
    public void Initialize(int size)
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(size, size);
    }
    void FollowTarget()
    {
        if (target == null) return;
        transform.position = target.position;
    }
}
