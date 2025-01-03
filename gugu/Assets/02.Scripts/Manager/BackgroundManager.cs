using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundManager : TSingletonMono<BackgroundManager>
{
    #region Fields
    BackgroundBlock[] backgroundBlocks;
    GameObject grid;
    #endregion

    #region Initialize Method
    protected override void OnInitialize()
    {
        var clonedObject = Instantiate(Resources.Load<GameObject>("Background/Grid"), transform);
        grid = clonedObject;
        backgroundBlocks = grid.transform.GetComponentsInChildren<BackgroundBlock>();
        var spawnArea = ActorManager.Instance.SpawnArea.transform;

        for (int i=0;i<backgroundBlocks.Length;i++)
            backgroundBlocks[i].Initialize(spawnArea,i);

        IsLoad = true;
    }
    #endregion

    #region Background Method
    public void ShowBackgroundByStage(string stagePath)
    {
        var backgroundTile = AddressableSystem.GetBackground(stagePath);

        grid.SetActive(true);
        foreach (var block in backgroundBlocks)
        {
            block.SetBackground(backgroundTile);
            backgroundTile.m_TilingRules[0].m_PerlinScale = Random.Range(1, 10) * 0.1f;
        }
    }
    public void HideBackground()
    {
        grid.SetActive(false);
        foreach (var block in backgroundBlocks)
        {
            block.ResetBackground();
        }
    }
    #endregion
}
