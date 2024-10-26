using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;

public class TitleScene : BaseScene
{
    Button button_NextScene;
    //나중에 퍼센테이지로 변경할것 .
    TextMeshProUGUI text_Description;
    
    public override void StartScene()
    {
        DontDestroyOnLoad(gameObject);
        button_NextScene = transform.GetComponentInChildren<Button>();
        button_NextScene.onClick.AddListener(() => AsyncSceneChange<MainScene>().Forget());
        button_NextScene.gameObject.SetActive(false);
        text_Description = transform.Find("TitleUI/Panel_Progress/Text_Progress").GetComponent<TextMeshProUGUI>();
        InitManager().Forget();
    }
    async UniTask InitManager()
    {
        text_Description.text = "Waiting ... ";
        RuntimePreference.Instance.Initialize();
        await UniTask.WaitUntil(() => RuntimePreference.Instance.IsLoad);

        CameraManager.Instance.Initialize();
        await UniTask.WaitUntil(() => CameraManager.Instance.IsLoad);

        DataManager.Instance.InitAddressableSystem();
        await UniTask.WaitUntil(() => DataManager.AddressableSystem.IsLoad);

        DataManager.Instance.Initialize();
        await UniTask.WaitUntil(() => DataManager.Instance.IsLoad);

        LocalizingManager.Instance.Initialize();
        await UniTask.WaitUntil(() => LocalizingManager.Instance.IsLoad);

        UIManager.Instance.Initialize();//Initialize LocalizingManager First
        await UniTask.WaitUntil(() => UIManager.Instance.IsLoad);

        SpawnManager.Instance.Initialize();
        await UniTask.WaitUntil(() => SpawnManager.Instance.IsLoad);

        BackgroundManager.Instance.Initialize();//Initialize SpawnManager First
        await UniTask.WaitUntil(() => BackgroundManager.Instance.IsLoad);

        text_Description.text = "Complete";
        button_NextScene.gameObject.SetActive(true);
    }
}
