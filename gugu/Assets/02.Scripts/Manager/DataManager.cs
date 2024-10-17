using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : TSingletonMono<DataManager>
{
    public static AddressableSystem AddressableSystem;
    public static LocalizingTable LocalizingTable;
    public static CharacterTable CharacterTable;
    public static EnemyTable EnemyTable;

    protected override void OnInitialize()
    {
        LocalizingTable = LoadTable<LocalizingTable>(eTableName.LocalizingTable);
        LocalizingTable.Reload();
        CharacterTable = LoadTable<CharacterTable>(eTableName.CharacterTable);
        CharacterTable.Reload();
        EnemyTable = LoadTable<EnemyTable>(eTableName.EnemyTable);
        EnemyTable.Reload();
        IsLoad = true;      
    }
    public void InitAddressableSystem()
    {
        AddressableSystem = new AddressableSystem();
        AddressableSystem.Initialize();
    }

    public T LoadTable<T>(eTableName name, bool isReload = false) where T : TableBase, new()
    {
        T t = new T();
        t.SetTableName = name.ToString();
        return t;
    }
}

