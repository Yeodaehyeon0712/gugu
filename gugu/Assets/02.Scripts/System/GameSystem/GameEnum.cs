
#region Actor
public enum eActorType:uint
{
    None,
    Character,
    Enemy,
    End
}
public enum eActorState
{
    None,
    Battle,
    Death,
    Inactive,
}
public enum eComponent
{
    SkinComponent,
    ControllerComponent,
    StatComponent,
    SkillComponent,
}
public enum eCharacterAnimState
{
    Idle ,
    Move ,
    Hit ,
    Death ,
}
public enum eSkillState
{
    None,
    Using,
    Cooltime,
    Inactive,
}
public enum eStatusType
{
    None,
    MaxHP,
    MoveSpeed,
    End
}
public enum eCalculateType
{
    Flat,
    Percentage
}

#endregion

#region Data
[System.Flags]
public enum eTableName
{
    LocalizingTable = 1 << 0,
    CharacterTable = 1 << 1,
    EnemyTable  =   1 << 2,
    WaveTable = 1 << 3,
    StageTable = 1 << 4,
    SkillTable=1<<5,
    EffectTable=1<<6,
    StatusTable=1<<7,
    All = ~0,
}

enum eAddressableState
{
    None,
    Initialized,
    FindPatch,
    DownloadDependencies,
    LoadMemory,
    TableMemory,
    AnimatorMemory,
    BackgroundMemory,
    Complete,
}
#endregion

#region Localizing
public enum eLanguage
{
    EN,     //English
    KR,     //Korean
    End
}
#endregion

#region Effect
public enum eEffectType
{
    None,
    Projectile,
    Crash,
    Shape,//To Do :추후 더 좋은 이름으로 변경 하자 
    End
}
public enum eEffectChainCondition
{
    None,
    Enable,
    Disable,
    Overlap,
}
[System.Flags]
public enum eEffectAttribute
{
    Duration = 1 << 0,
    Velocity = 1 << 1,
    Overlap = 1 << 2,
    PostEffect = 1 << 3,
}
public enum ePostEffectType
{
    None,
    KnockBack,
    Stun
}
#endregion

#region Manager
#endregion

#region Pattern
#endregion

#region Scene
#endregion

#region Stage
public enum eStageType
{
    Normal,
    End
}
public enum eStageResultState
{
    InProgress,
    Victory,
    Defeat,
}
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

#region Preference
public enum ePreference
{
    BGM,
    SFX,
    Alram,
    JoyStick,
    Vibration,
    Effect
}
#endregion
#endregion

#region UI
[System.Flags]//31 is Maximum Value
public enum eUI
{
    Controller = 1 << 0,
    BattleState = 1 << 1,
    BattlePausePopUp = 1 << 2,
    AlramPopUp = 1 << 3,
    SettingPopUp=1<<4,
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