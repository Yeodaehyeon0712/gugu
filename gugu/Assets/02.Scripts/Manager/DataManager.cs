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
    public static WaveTable WaveTable;
    public static StageTable StageTable;//Initialize WaveTable First
    public static SkillTable SkillTable;
    public static EffectTable EffectTable;
    public static StatusTable StatusTable;
    public static ItemTable ItemTable;
    public static EquipmentTable EquipmentTable;

    protected override void OnInitialize()
    {
        LocalizingTable = LoadTable<LocalizingTable>(eTableName.LocalizingTable);
        LocalizingTable.Reload();
        CharacterTable = LoadTable<CharacterTable>(eTableName.CharacterTable);
        CharacterTable.Reload();
        EnemyTable = LoadTable<EnemyTable>(eTableName.EnemyTable);
        EnemyTable.Reload();
        WaveTable = LoadTable<WaveTable>(eTableName.WaveTable);
        WaveTable.Reload();
        StageTable = LoadTable<StageTable>(eTableName.StageTable);
        StageTable.Reload();
        SkillTable = LoadTable<SkillTable>(eTableName.SkillTable);
        SkillTable.Reload();
        EffectTable = LoadTable<EffectTable>(eTableName.EffectTable);
        EffectTable.Reload();
        StatusTable = LoadTable<StatusTable>(eTableName.StatusTable);
        StatusTable.Reload();
        ItemTable = LoadTable<ItemTable>(eTableName.ItemTable);
        ItemTable.Reload();
        EquipmentTable= LoadTable<EquipmentTable>(eTableName.EquipmentTable);
        EquipmentTable.Reload();
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

