using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    //���� ������ ������ �ֵ� �ٽ� �� �ڸ��� ���� / ���� ����Ʈ�� �������ش� . 
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
    //�÷��̾�� �ݴ��� �������� ���� ..�ϴ� �޼��嵵 �ʿ��� (���� �̵���)
}
