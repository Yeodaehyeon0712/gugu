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
        InitializeApplicationSystem().Forget();

    }
    async UniTask InitializeApplicationSystem()
    {
        SceneManager.Instance.Initialize();
        await UniTask.WaitUntil(() => SceneManager.Instance.IsLoad);
        GameConst.Initialize();
        StartScene();
    }
    public override void StartScene()
    {
        DontDestroyOnLoad(gameObject);
        _logo.DOFade(1f, 2f).SetEase(_fadeCurve).OnComplete(() => SceneManager.Instance.AsyncSceneChange<TitleScene>().Forget());
    }
}
