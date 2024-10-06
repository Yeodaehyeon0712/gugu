using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundBlock : RepositionObject
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

    #region Reposition Method
    protected override void Reposition()
    {
        Vector2 playerPos = target.position; 
        Vector2 myPos = transform.position;
        Vector2 diff = playerPos - myPos;

        bool isHorizontal = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);
        Vector2 movementDirection = isHorizontal ? Vector2.right : Vector2.up;
        float movementValue = halfSize* 4 * Mathf.Sign(isHorizontal ? diff.x : diff.y);

        transform.position += (Vector3)(movementDirection * movementValue);
    }
    #endregion
}
