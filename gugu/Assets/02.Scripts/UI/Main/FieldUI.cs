using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldUI : MonoBehaviour
{
    #region Fields
    Dictionary<eFieldUI, MemoryPool<BaseFieldUI>> memoryPoolDic=new Dictionary<eFieldUI, MemoryPool<BaseFieldUI>>(2);
    protected Dictionary<eFieldUI,List< BaseFieldUI>> spawnedObjectDic = new Dictionary<eFieldUI, List<BaseFieldUI>>();
    #endregion

    #region Init Method
    public void Initialize()
    {
        memoryPoolDic.Add(eFieldUI.HPBar, new MemoryPool<BaseFieldUI>(10));
        memoryPoolDic.Add(eFieldUI.DamageText, new MemoryPool<BaseFieldUI>(20));
        spawnedObjectDic.Add(eFieldUI.HPBar, new List<BaseFieldUI>());
        transform.SetParent(CameraManager.Instance.GetCamera(eCameraType.MainCamera).transform);
    }
    #endregion

    #region Field UI Method
    public T FindFieldUI<T>(eFieldUI type) where T : BaseFieldUI
    {
        T ui = memoryPoolDic[type].GetItem() as T;
        if (ui != null)
            return ui;

        ui = Instantiate(Resources.Load<T>("UI/FieldUI/" + type.ToString()), transform);
        ui.Init(memoryPoolDic[type].Register);
        spawnedObjectDic[type].Add(ui);
        return ui;
    }
    public void SetHPBar(Actor target)
    {
        FindFieldUI<FieldUI_HPBar>(eFieldUI.HPBar).Enable(target);
    }
    public void SetDamageText(Vector3 pos, double damage, bool isCritical, eActorType type)
    {

        //FindFieldUI<FieldUI_DamageText>(EFieldUIType.DamageText).Enabled2(pos, damage, isCritical, type);
    }
    public void Clear()
    {
        foreach (var fieldUIList in spawnedObjectDic.Values)
        {
            foreach(var fieldUI in fieldUIList)
                fieldUI.Disable();
        }
        spawnedObjectDic.Clear();
    }
    #endregion
}
