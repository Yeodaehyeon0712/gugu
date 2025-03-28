public static class Player 
{
    #region Fields
    public static IngameDataProperty InGameData => ingameDataProperty;
    static IngameDataProperty ingameDataProperty;
    public static bool IsLoad = false;
    public static Character PlayerCharacter;

    //Level
    static int currentLevel;
    static int CurrentLevel
    {
        get => currentLevel;
        set
        {
            currentLevel = value;
            battleStateUI.SetLevel(currentLevel);

        }
    }
    //Exp
    static float currentExp;
    static float CurrentExp
    {
        get => currentExp;
        set
        {
            currentExp = value;
            var per = UnityEngine.Mathf.Clamp01(currentExp / nextLevelExp);
            battleStateUI.SetLevelPercentage(per);
        }
    }

    static float nextLevelExp;

    //UI Instance
    static BattleStateUI battleStateUI;
    #endregion

    #region Init Method
    public static void Initialize()
    {
        ingameDataProperty = new IngameDataProperty();
        battleStateUI = UIManager.Instance.BattleStateUI;//If Player is initialized before UIManager, a problem can occur.
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
        CurrentLevel = 1;
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
        CurrentExp += exp;

        if (currentExp >= nextLevelExp)
            LevelUp();
    }
    static void LevelUp()
    {
        CurrentLevel++;
        UIManager.Instance.LevelUpPopUpUI.Enable();
        SetNextLevelExp();
    }
    static void SetNextLevelExp()
    {
        var nextLevel = currentLevel + 1;
        nextLevelExp = 10 + 10*(nextLevel - 1);
        CurrentExp = 0;
    }
    #endregion
}
