public static class Player 
{
    #region Fields
    public static IngameDataProperty InGameData => ingameDataProperty;
    static IngameDataProperty ingameDataProperty;
    public static bool IsLoad = false;
    public static Character PlayerCharacter;

    //Level
    public static float Level => currentLevel;
    static float currentLevel;
    static float currentExp;
    static float nextLevelExp;
    #endregion

    #region Init Method
    public static void Initialize()
    {
        ingameDataProperty = new IngameDataProperty();
        IsLoad = true;
    }
    public static void RegisterPlayer(Character actor)
    {
        //Player
        PlayerCharacter = actor;
        ingameDataProperty.InitializeData();
        InitLevel();

        //Camera
        CameraManager.Instance.RegisterFollowTarget(PlayerCharacter.transform);
        //Spawn Area
        ActorManager.Instance.RegisterSpawnAreaParent(PlayerCharacter.transform);
        //UI
        UIManager.Instance.FieldUI.SetHPBar(PlayerCharacter);
    }
    public static void UnRegisterPlayer()
    {
        PlayerCharacter = null;
        ingameDataProperty.CleanData();
        CleanLevel();

        //Camera
        CameraManager.Instance.RegisterFollowTarget(null);
        //SpawnArea
        ActorManager.Instance.RegisterSpawnAreaParent(null);
        //UI
        UIManager.Instance.FieldUI.Clear();//To Do ::Find More Good Way
    }
    #endregion

    #region LevelUp Method
    static void InitLevel()
    {
        currentLevel = 1;
        currentExp = 0;
        SetNextLevelExp();
    }
    static void CleanLevel()
    {
        currentExp = 0;
        currentLevel = 0;
        nextLevelExp = 0;
    }
    // To Do : 연속 레벨업 고려하기
    public static void GetExp(float exp)
    {
        currentExp += exp;
        if (currentExp >= nextLevelExp)
            LevelUp();
    }
    static void LevelUp()
    {
        currentExp = 0;
        currentLevel++;
        SetNextLevelExp();
        UIManager.Instance.LevelUpPopUpUI.Enable();
    }
    static void SetNextLevelExp()
    {
        var nextLevel = currentLevel + 1;
        nextLevelExp = 10 + 10*(nextLevel - 1);
    }
    #endregion
}
