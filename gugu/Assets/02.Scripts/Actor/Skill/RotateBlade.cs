using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RotateBlade : BaseSkill
{
    #region Field
    Transform bladeParent;
    float rotateRadius = 1.5f;
    float rotationAnglePerSec = 30f;
    List<BaseEffect> bladeList = new List<BaseEffect>();
    #endregion

    #region Init Method
    public RotateBlade(long index, Data.SkillData skillData):base(index,skillData)
    {

    }
    async UniTask GenerateBlade()
    {
        int bladeCount = skillData.GetIntCoefficient(level);
        while (bladeList.Count < bladeCount)
        {
            var blade = await EffectManager.Instance.SpawnEffect(eEffectType.Crash,1, Vector3.zero, bladeParent);
            var damage = 100 * skillData.GetCoefficient(level);//이건 다시 해야해
            blade.SetOverlapEvent(eActorType.Enemy,owner.WorldID,damage,true);
            bladeList.Add(blade);
        }
        var angleInterval = (Mathf.PI * 2 / bladeCount);

        for (int i = 0; i < bladeList.Count; i++)
        {
            float targetAngle = i * angleInterval;
            var angleIntervalInDegrees = targetAngle * Mathf.Rad2Deg - 90f;
            Debug.Log("A" + angleIntervalInDegrees);
            bladeList[i].transform.localRotation = Quaternion.Euler(0, 0, angleIntervalInDegrees);
            bladeList[i].transform.localPosition = new Vector3(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * rotateRadius;
        }
    }
    #endregion

    #region Skill Method
    protected override void OnRegister()
    {
        bladeParent = new GameObject("BladeParent").transform;
        bladeParent.SetParent(owner.transform);
        bladeParent.localPosition = Vector3.zero;
        bladeParent.localRotation = Quaternion.identity;
        GenerateBlade().Forget();
        Debug.Log("등록 완료");
    }
    protected override void OnUnRegister()
    {
        foreach(var a in bladeList)
        {
            a.Disable();
        }
        //GameObject.Destroy(bladeParent);
    }
    protected override void OnUse()
    {
        bladeParent.gameObject.SetActive(true);
    } 
    protected override void OnUpdate(float deltaTime)
    {
        bladeParent.transform.Rotate(Vector3.forward, rotationAnglePerSec * deltaTime);
    }

    protected override void OnStop()
    {
        bladeParent.gameObject.SetActive(false);
    }
    protected override void OnLevelUp()
    {
        GenerateBlade().Forget();
    }
    #endregion
}

