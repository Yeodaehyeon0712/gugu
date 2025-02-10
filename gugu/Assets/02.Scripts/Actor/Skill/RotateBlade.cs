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
    //void GenerateBlade(uint level)
    //{
    //    Debug.Log("Į�� ����");
    //    var prefab = Resources.Load<GameObject>("Blade");
    //    int bladeCount = skillData.GetIntCoefficient(level);

    //    while (bladeList.Count < bladeCount)
    //    {
    //        var newBlade = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, bladeParent);
    //        bladeList.Add(newBlade);
    //    }

    //    var angleInterval = (Mathf.PI * 2 / bladeCount);

    //    for (int i = 0; i < bladeList.Count; i++)
    //    {
    //        float targetAngle = i * angleInterval;
    //        var angleIntervalInDegrees = targetAngle * Mathf.Rad2Deg - 90f;
    //        Debug.Log("A" + angleIntervalInDegrees);
    //        bladeList[i].transform.localRotation = Quaternion.Euler(0, 0, angleIntervalInDegrees);
    //        bladeList[i].transform.localPosition = new Vector3(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle)) * rotateRadius;
    //    }
    //    //���⼭ �����ϴ� Į���� ����Ʈ�� ����, �������� �ɾ��ش� .������ �ϸ� �ٷ� ... 
    //}
    async UniTask GenerateBlade()
    {
        int bladeCount = skillData.GetIntCoefficient(level);
        while (bladeList.Count < bladeCount)
        {
            var blade = await EffectManager.Instance.SpawnEffect(1, Vector3.zero, bladeParent);
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
        Debug.Log("��� �Ϸ�");
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
    protected override void OnUpdate()
    {
        bladeParent.transform.Rotate(Vector3.forward, rotationAnglePerSec * Time.deltaTime);
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

