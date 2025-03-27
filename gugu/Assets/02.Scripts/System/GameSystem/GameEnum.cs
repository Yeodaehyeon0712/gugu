
#region Actor
public enum eActorType:uint
{
    Character,
    Enemy,
}
public enum eActorState
{
    None,
    Spawn,
    Active,
    Death,
    Clean,
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
    MaxHP,
    Recovery,
    Armor,
    MoveSpeed,
    Might,
    AttackSpeed,
    Duration,
    Area,
    CoolTime,
    Amount,
    Revival,
    Magnet,
    Luck,
    Growth,
    Greed,
    Curse,
}
public enum eCalculateType
{
    Flat,
    Percentage
}
public enum eAttachmentTarget
{
    OverHead,    
    End,
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
    ItemTable = 1 << 8,
    EquipmentTable=1<<9,
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
    IconMemory,
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
    Projectile,
    Crash,
    Shape,//To Do :추후 더 좋은 이름으로 변경 하자 
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
public enum eStageFrameworkState
{
    None,
    SetUp,
    InProgress,
    Victory,
    Defeat,
    Cancel,
    Clean,
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

#region Item
public enum eItemType
{
    Gem,
    Heart,
}
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
    LevelUpPopUp=1<<5,
    ResultPopUp = 1 << 6,
    MenuButton=1<<7,
    Lobby=1<<8,
    PlayerInfo=1<<9,
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
public enum eFieldUI
{
    HPBar,
    DamageText,
}
public enum eLobbyUI
{
    Store,
    Temp1,
    Battle,
    Temp2,
    Enforce,
}
#endregion