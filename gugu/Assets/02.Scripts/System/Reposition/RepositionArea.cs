using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionArea : MonoBehaviour
{
    //��� �÷��̾ �����Ұ��̴� .
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
