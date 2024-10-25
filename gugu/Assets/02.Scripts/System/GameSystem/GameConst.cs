using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    #region Background
    public static int BgBlockSideSize=30;
    #endregion

    #region Camera
    public static Vector2 targetResolution = new Vector2(1080, 1920);
    public static float defaultOrthoSize = 15.0f;
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
