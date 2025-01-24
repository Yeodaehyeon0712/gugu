using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Actor
{
    #region Fields
    public SkillComponent Skill => skillComponent;
    [SerializeField] protected SkillComponent skillComponent;
    public Data.CharacterData characterData;
    #endregion

    #region Character Method

    #endregion
    protected override void InitializeComponent()
    {
        //Default Component
        skinComponent = new SkinComponent(this, AddressableSystem.GetAnimator(DataManager.CharacterTable[index].AnimatorPath));
        statusComponent = new CharacterStatusComponent(this);
        controllerComponent = new CharacterControllerComponent(this);

        //Character Component
        skillComponent = new SkillComponent(this);
        characterData = DataManager.CharacterTable[index];
    }
}
