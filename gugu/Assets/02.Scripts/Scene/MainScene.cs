using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class MainScene : BaseScene
{
    public Character a;
    public override void StartScene()
    {
        StageManager.Instance.SetupStage(eStageType.Normal, 1);
        Invoke("GetSkill", 5f);
        Invoke("LevelUpSkill", 10f);

    }
    public void GetSkill()
    {
        a= GameObject.Find("Character(Clone)").GetComponent<Character>();
        Debug.Log(a.Skill+"123");
        a.Skill.GetSkill(1);
    }

    public void LevelUpSkill()
    {
        a.Skill.LevelUpSkill(0);
    }
}
