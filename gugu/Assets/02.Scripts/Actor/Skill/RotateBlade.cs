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
    List<GameObject> bladeList = new List<GameObject>();
    #endregion

    #region Init Method
    public RotateBlade(long index, Data.SkillData skillData):base(index,skillData)
    {

    }
    void GenerateBlade(int level)
    {
        var prefab = Resources.Load<GameObject>("Blade");
        int bladeCount = skillData.GetIntCoefficient(level);

        while (bladeList.Count < bladeCount)
        {
            var newBlade = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, bladeParent);
            bladeList.Add(newBlade);
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
        //여기서 생성하는 칼날은 이펙트로 생성, 데미지를 심어준다 .레벨업 하면 바로 ... 
    }

    #endregion

    #region Skill Method
    protected override void OnRegister()
    {
        bladeParent = new GameObject("BladeParent").transform;
        bladeParent.SetParent(owner.transform);
        bladeParent.localPosition = Vector3.zero;
        bladeParent.localRotation = Quaternion.identity;
        GenerateBlade(level);
    }
    protected override async UniTask OnUsingSequenceAsync()
    {
        bladeParent.gameObject.SetActive(true);
        while (processState == eSkillState.Using)
        {
            await UniTask.Yield();
            bladeParent.transform.Rotate(Vector3.forward, rotationAnglePerSec * Time.deltaTime);
        }
    }
    protected override void OnStop()
    {
        bladeParent.gameObject.SetActive(false);
    }
    protected override void OnLevelUp()
    {
        GenerateBlade(level);
    }
    protected override void OnUnRegister()
    {
        //GameObject.Destroy(bladeParent);
    }
    #endregion
}

