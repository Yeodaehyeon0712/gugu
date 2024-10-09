using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst
{
    public static float mapSize=30f;

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
