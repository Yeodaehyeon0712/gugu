using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundBlock : RepositionObject
{
    #region Fields
    Tilemap tilemap;
    Vector3 firstPosition;
    int apothem;
    #endregion

    #region Background Method
    public override void Initialize(Transform spawnArea)
    {
        base.Initialize(spawnArea);
        firstPosition = transform.position;
        tilemap = GetComponent<Tilemap>();
        this.apothem = GameConst.BgBlockSideSize/2;
    }
    public void SetBackground(RuleTile tile)
    {
        for (int y = -apothem; y < apothem; y++)
        {
            for (int x = -apothem; x < apothem; x++)
                tilemap.SetTile(new Vector3Int(x, y, 0), tile);
        }
        tilemap.RefreshAllTiles();
    }
    public void ResetBackground()
    {
        transform.position = firstPosition;
        //하위 아이템들 삭제두 있겠지 ..
    }
    
    #endregion

    #region Reposition Method
    protected override void Reposition()
    {
        Vector2 spawnAreaPos = spawnArea.position; 
        Vector2 myPos = transform.position;
        Vector2 diff = spawnAreaPos - myPos;

        bool isHorizontal = Mathf.Abs(diff.x) > Mathf.Abs(diff.y);
        Vector2 movementDirection = isHorizontal ? Vector2.right : Vector2.up;
        float movementValue = apothem* 4 * Mathf.Sign(isHorizontal ? diff.x : diff.y);

        transform.position += (Vector3)(movementDirection * movementValue);
    }
    #endregion
}
