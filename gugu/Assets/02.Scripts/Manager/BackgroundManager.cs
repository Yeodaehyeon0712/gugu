using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundManager : TSingletonMono<BackgroundManager>
{
    #region Fields
    //���߿� int�� Stage Ÿ������ ���� 
    Dictionary<int, RuleTile> backgroundTiles = new Dictionary<int, RuleTile>();
    BackgroundBlock[] backgroundBlocks;
    GameObject grid;
    int blockSideSize;
    #endregion

    #region Backgrounds Method
    protected override void OnInitialize()
    {
        var clonedObject = Instantiate(Resources.Load<GameObject>("Background/Grid"), transform);
        grid = clonedObject;
        backgroundBlocks = grid.transform.GetComponentsInChildren<BackgroundBlock>();
        blockSideSize = GameConst.BgBlockSideSize;

        foreach (var block in backgroundBlocks)
            block.Initialize(blockSideSize/2);

        LoadBackgroundTile();
        IsLoad = true;
    }
    void LoadBackgroundTile()
    {
        //�������� ���̺��� �ε����� ��� �� Ÿ���� ��ųʸ��� �ε�
        for(int i=1;i<=3;i++)
        {
            backgroundTiles.Add(i,Resources.Load<RuleTile>($"Background/BackgroundTile{i}"));
        }
    }
    public void ShowBackgroundByStage(int stageIndex)
    {
        if(backgroundTiles.TryGetValue(stageIndex,out RuleTile value)==false)
        {
            Debug.LogWarning($"Wrong BackgroundIndex {stageIndex}");
            return;
        }
        grid.SetActive(true);
        foreach (var block in backgroundBlocks)
        {
            block.ShowBackground(value);
            value.m_TilingRules[0].m_PerlinScale= Random.Range(1,10)*0.1f;
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
