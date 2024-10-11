
#region Actor
public enum eComponent
{
    ControllerComponent,
    SkinComponent,
}
public enum eCharacterAnimState
{
    Idle ,
    Move ,
    Hit ,
    Death ,
}
#endregion

#region Data
[System.Flags]
public enum eTableName
{
    LocalizingTable = 1 << 1,
    All = ~0,
}

enum eAddressableState
{
    None,
    Initialized,
    FindPatch,
    Downloading,
    LoadMemory,
    ModelMemory,
    TableMemory,
    Complete,
}
#endregion

#region Localizing
public enum eLanguage
{
    English,
    Korean,
    End
}
#endregion

#region Manager
#endregion

#region Pattern
#endregion

#region Scene
#endregion

#region System
#region Background
#endregion

#region Camera
public enum eCameraType
    {
        MainCamera,
        VirtualCamera,
    }
#endregion

#region Reposition
#endregion
#endregion

#region UI
[System.Flags]
public enum eUI
{
    Main = 1 << 0,
    Controller = 1 << 1,
    Setting = 1 << 2,
}
public enum eMovableUIDir
{
    None,
    LeftToRight,
    RightToLeft,
    TopToBottom,
    BottomToTop
}
enum LetterBoxDirection
{
    Top,
    Bottom,
}
#endregion