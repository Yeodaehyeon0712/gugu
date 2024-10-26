using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
[RequireComponent(typeof(CanvasGroup))]
public abstract class MovableUI : BaseUI
{
    #region Variables
    [SerializeField]eMovableUIDir dir;
    RectTransform rectTransform;
    Vector2 canvasSize;
    CanvasGroup canvasGroup;
    GameObject raycastBlock;
    #endregion
    public override BaseUI Initialize()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasSize = UIManager.Instance.GameUI.GetComponent<CanvasScaler>().referenceResolution;
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.interactable = false;
        raycastBlock = transform.parent.Find("RaycastBlock").gameObject;
        SetFirstPosition();
        return base.Initialize();
    }
    public override void Enable()
    {
        base.Enable();
        raycastBlock.SetActive(true);
    }
    public override void Disable()
    {
        base.Disable();
        raycastBlock.SetActive(false);
    }
    #region Movable Func
    void SetFirstPosition()
    {
        rectTransform.localPosition = dir switch
        {
            eMovableUIDir.LeftToRight => new Vector2(-canvasSize.x, 0),
            eMovableUIDir.RightToLeft => new Vector2(canvasSize.x, 0),
            eMovableUIDir.TopToBottom => new Vector2(0, canvasSize.y),
            eMovableUIDir.BottomToTop => new Vector2(0, -canvasSize.y),
            _ => Vector2.zero
        };
    }
    public void MoveIn(float duration=1f)
    {
        Enable();
        switch (dir)
        {
            case eMovableUIDir.LeftToRight:
            case eMovableUIDir.RightToLeft:
                rectTransform.DOLocalMoveX(0, duration).OnComplete(() => canvasGroup.interactable = true);
                break;
            case eMovableUIDir.TopToBottom:
            case eMovableUIDir.BottomToTop:
                rectTransform.DOLocalMoveY(0, duration).OnComplete(() => canvasGroup.interactable = true);
                break;
            default:return;
        }
    }
    public void MoveOut(float duration=1f)
    {
        canvasGroup.interactable = false;
        switch (dir)
        {
            case eMovableUIDir.LeftToRight:
                rectTransform.DOLocalMoveX(-canvasSize.x, duration).onComplete=Disable;
                break;
            case eMovableUIDir.RightToLeft:
                rectTransform.DOLocalMoveX(canvasSize.x, duration).onComplete=Disable;
                break;
            case eMovableUIDir.TopToBottom:
                rectTransform.DOLocalMoveY(canvasSize.y, duration).onComplete = Disable;
                break;
            case eMovableUIDir.BottomToTop:
                rectTransform.DOLocalMoveY(-canvasSize.y, duration).onComplete = Disable;
                break;
            default: return;
        }
    }
    #endregion
}

