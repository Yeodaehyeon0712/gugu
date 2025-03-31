public static class Player 
{
    #region Fields
    public static IngameDataProperty InGameData => ingameDataProperty;
    static IngameDataProperty ingameDataProperty;
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
        //Player
        PlayerCharacter = actor;
        ingameDataProperty.InitializeData();
    }
    public static void ActivePlayer()
    {
        PlayerCharacter.ActiveActor();
        //UI
        UIManager.Instance.FieldUI.SetHPBar(PlayerCharacter);
        //Camera
        CameraManager.Instance.RegisterFollowTarget(PlayerCharacter.transform);
        //Spawn Area
        ActorManager.Instance.RegisterSpawnAreaParent(PlayerCharacter.transform);
    }
    public static void UnRegisterPlayer()
    {
        PlayerCharacter = null;
        ingameDataProperty.CleanData();

        //Camera
        CameraManager.Instance.RegisterFollowTarget(null);
        //SpawnArea
        ActorManager.Instance.RegisterSpawnAreaParent(null);
        //UI
        UIManager.Instance.FieldUI.Clear();//To Do ::Find More Good Way

    }
    #endregion
}
