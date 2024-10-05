using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundBlock : MonoBehaviour
{
    #region Fields
    Tilemap tilemap;
    Vector3 firstPosition;
    int halfSize;
    #endregion

    #region Background Method
    public void Initialize(int halfSize)
    {
        firstPosition = transform.position;
        tilemap = GetComponent<Tilemap>();
        this.halfSize = halfSize;
    }
    public void ShowBackground(RuleTile tile)
    {
        SetBackground(tile);
        gameObject.SetActive(true);
    }
    public void ResetBackground()
    {
        transform.position = firstPosition;
        //하위 아이템들 삭제두 있겠지 ..
    }
    void SetBackground(RuleTile tile)
    {
        for (int y = -halfSize; y < halfSize; y++)
        {
            for (int x = -halfSize; x < halfSize; x++)
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
        }
        tilemap.RefreshAllTiles();
    }
    #endregion

    #region Move Method
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area") == false) return;

        Move();
    }
    void Move()
    {
        Vector2 playerPos = Vector2.zero;// player.position; // Assuming player is a Transform reference
        Vector2 myPos = transform.position;
        Vector2 diff = playerPos - myPos;


        bool isHorizontalDominant = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);
        var movementDirection = isHorizontalDominant ? Vector3.right : Vector3.up; 
        var movementDifference = Mathf.Sign(isHorizontalDominant ? diff.x : diff.y); 

        transform.Translate(movementDirection * movementDifference *40 * Time.deltaTime); // Move the transform
    }
    #endregion
}
