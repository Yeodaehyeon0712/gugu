using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Player 
{
    //Data
    public static IngameDataProperty InGameData => ingameDataProperty;
    static IngameDataProperty ingameDataProperty;

    #region Fields
    public static bool IsLoad = false;
    public static Character PlayerCharacter;
    #endregion

    #region Init Method
    public static void Initialize()
    {
        ingameDataProperty = new IngameDataProperty();
        IsLoad = true;
    }
    public static void RegisterPlayer(Character actor)
    {
        PlayerCharacter = actor;
        ingameDataProperty.InitializeIngameData();
        //일단 이거 위치 고민
        currentLevel = 1;
        currentExp = 0;
        SetNextLevelExp();      
        //Camera
        CameraManager.Instance.RegisterFollowTarget(PlayerCharacter.transform);
        //Spawn Area
        ActorManager.Instance.RegisterSpawnAreaParent(PlayerCharacter.transform);

        UIManager.Instance.FieldUI.SetHPBar(PlayerCharacter);
    }
    public static void UnRegisterPlayer()
    {
        if (PlayerCharacter == null) return;
        //Camera
        CameraManager.Instance.RegisterFollowTarget(null);
        //SpawnArea
        ActorManager.Instance.RegisterSpawnAreaParent(null);

        //UI
        UIManager.Instance.FieldUI.FindFieldUI<FieldUI_HPBar>(eFieldUI.HPBar).Disable();//이것도 수정

        //이것도 고민해바 , 이미 죽었다면 ?
        PlayerCharacter.Death(0f);
        PlayerCharacter = null;
    }
    #endregion

    #region LevelUp
    static float currentExp;
    static float nextLevelExp;
    static float currentLevel;
    public static float Level => currentLevel;

    public static void GetExp(float exp)
    {
        currentExp += exp;
        if (currentExp >= nextLevelExp)
            LevelUp();
    }
    public static void LevelUp()
    {
        currentLevel++;
        SetNextLevelExp();
        UIManager.Instance.LevelUpPopUpUI.Enable();
    }
    public static void SetNextLevelExp()
    {
        var nextLevel = currentLevel + 1;
        nextLevelExp = 10 + 5*(nextLevel - 1);
    }
    #endregion
}
