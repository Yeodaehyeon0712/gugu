using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    #region Stage
    public static float subWaveTime = 30f;
    public static float spawnInterval = 3f;
    #endregion

    #region Background
    public static int BgBlockSideSize = 40;
    public static Vector2[] BgBlockPositions =
    {
    new Vector2(1, 1),
    new Vector2(-1, 1),
    new Vector2(-1, -1),
    new Vector2(1, -1)
    };
    #endregion

    #region Camera
    public static Vector2 defaultResolution = new Vector2(1080, 1920);
    public static int defaultMinimumPPU = 48;
    public static int[] zoomArray ={1, 2, 3};
    #endregion

    #region Path
#if UNITY_EDITOR
    public static readonly string CacheDirectoryPath = "Cache/";
    public static readonly string LogDirectoryPath = CacheDirectoryPath + "/Log/";
#else
    public static readonly string CacheDirectoryPath = UnityEngine.Application.persistentDataPath + "/Cache/";
    public static readonly string LogDirectoryPath = CacheDirectoryPath + "/Log/";
#endif
    #endregion

    #region Enum Converter
    //public static Dictionary<string, eStatusType> StringToStatusType;


    public static void InitializeEnumConverter()
    {
       // OnGenerateEnumContainer(ref StringToStatusType);
        
    }
    public static void ClearEnumConverter()
    {
        //StringToStatusType = null;
        
    }
    static void OnGenerateEnumContainer<T>(ref Dictionary<string, T> container) where T : Enum
    {
        Type type = typeof(T);
        string[] names = Enum.GetNames(type);
        Array values = Enum.GetValues(type);
        container = new Dictionary<string, T>(names.Length);
        int count = 0;
        foreach (var element in values)
            container.Add(names[count++], (T)element);
    }
    #endregion
}
