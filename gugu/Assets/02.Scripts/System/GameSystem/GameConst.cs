using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    #region Data
    public static Dictionary<long, string> LongToString;
    public static Dictionary<string, long> StringToLong;
    #endregion

    #region Stage
    public static float subWaveTime = 30f;
    public static float spawnInterval = 3f;
    #endregion

    #region Status
    public static uint MaxStatusLevel=6;
    public static uint MaxSkillLevel = 6;
    public static int LevelUpSlotCount = 4;
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
    public static Dictionary<string, eEffectChainCondition> EffectChainType;
    public static Dictionary<string, eStatusType> StatusType;
    public static Dictionary<string, eCalculateType> CalculateType;
    public static Dictionary<string, eItemType> ItemType;


    public static void InitializeEnumConverter()
    {
        OnGenerateEnumContainer(ref EffectChainType);
        OnGenerateEnumContainer(ref StatusType);
        OnGenerateEnumContainer(ref CalculateType);
        OnGenerateEnumContainer(ref ItemType);
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

    public static void Initialize()
    {
        if (System.IO.Directory.Exists(CacheDirectoryPath) == false)
            System.IO.Directory.CreateDirectory(CacheDirectoryPath);

        StringToLong = new Dictionary<string, long>(200);
        LongToString = new Dictionary<long, string>(200);
        for (long i = 1; i < 201; ++i)
        {
            string key = i.ToString();
            StringToLong.Add(key, i);
            LongToString.Add(i, key);
        }
        InitializeEnumConverter();
        //for(eStatusType i = 0; i < eStatusType.End; ++i)
        //{
        //    Sprite sprite = Resources.Load<Sprite>("");
        //}
    }
}
