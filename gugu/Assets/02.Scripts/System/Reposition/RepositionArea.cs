using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionArea : MonoBehaviour
{
    BoxCollider2D boxCollider;
    public void Initialize(int size)
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(size, size);
    }
    public void RegisterParent(Transform parent)
    {
        //Area Must be a Child of the rigidBody
        transform.SetParent(parent);
    }
}
