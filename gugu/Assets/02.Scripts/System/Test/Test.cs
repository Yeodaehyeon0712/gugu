using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Test : MonoBehaviour
{
    #region Fields
    [SerializeField]int a;
    #endregion

    private void Start()
    {
        BackgroundManager.Instance.Initialize();
    }

    public void Push()
    {
        BackgroundManager.Instance.ShowBackgroundByStage(a);

    }

    
}