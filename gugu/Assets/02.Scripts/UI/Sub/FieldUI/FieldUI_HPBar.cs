using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FieldUI_HPBar : BaseFieldUI
{
    #region Fields
    Actor target;
    Transform pivotTrs;
    Image hpProgressBar;
    float currHPRate;
    #endregion

    #region Field UI Method
    public override BaseFieldUI Init(MemoryPool<BaseFieldUI>.del_Register register)
    {
        hpProgressBar = transform.Find("Img_ProgressImg").GetComponent<Image>();
        return base.Init(register);
    }
    public void Enable(Actor target)
    {
        this.target = target;
        pivotTrs = target.Attachment.GetAttachmentElement(eAttachmentTarget.OverHead).Transform;
        transform.position = pivotTrs.position;
        OnUpdateHPRate();
        base.Enable();
    }
    public override void Disable()
    {
        target = null;
        base.Disable();
    }
    #endregion

    #region HPBar Method
    void OnUpdateHPRate()
    {
        currHPRate = (float)(target.CurrentHP / target.Status.GetStatus(eStatusType.MaxHP));

        if (currHPRate != hpProgressBar.fillAmount)
            hpProgressBar.fillAmount = currHPRate;
    }
    #endregion

    #region Unity API   
    private void LateUpdate()
    {
        if (target == null || target.ActorState == eActorState.Death)
        {
            Disable();
            return;
        }

        if (transform.position != pivotTrs.position)
        {
            transform.position = pivotTrs.position;

        }

        OnUpdateHPRate();
    }
    #endregion
}
