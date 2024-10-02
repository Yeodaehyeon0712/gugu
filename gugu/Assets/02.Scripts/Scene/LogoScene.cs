using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

public class LogoScene : BaseScene
{
    [SerializeField] Image _logo;
    [SerializeField] AnimationCurve _fadeCurve;

    private void Awake()
    {
        _logo.color = new Color(1, 1, 1, 0);
        StartScene();
    }
    public override void StartScene()
    {
        DontDestroyOnLoad(gameObject);
        _logo.DOFade(1f, 2f).SetEase(_fadeCurve).OnComplete(() => AsyncSceneChange<TitleScene>().Forget());
    }
}
