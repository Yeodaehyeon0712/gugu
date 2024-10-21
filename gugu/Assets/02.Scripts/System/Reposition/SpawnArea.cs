using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    //나의 영역을 나가는 애들 다시 내 자리로 복귀 / 스폰 포인트를 리턴해준다 . 
    BoxCollider2D boxCollider;
    [SerializeField]Transform[] spawnPoints;//Drag in Insprector
    public void Initialize(int size)
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = new Vector2(size, size);
    }
    public void RegisterParent(Transform parent)
    {
        //Parent Must be have a rigidBody
        transform.SetParent(parent);
    }
    public Vector3 GetRandomPosition()
    {
        int randomValue = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomValue].position;
    }
    //플레이어와 반대의 포지션을 리턴 ..하는 메서드도 필요함 (몬스터 이동시)
}
