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
    //���߿� �ۼ��������� �����Ұ� .
    TextMeshProUGUI text_Description;
    
    public override void StartScene()
    {
        DontDestroyOnLoad(gameObject);
        button_NextScene = transform.GetComponentInChildren<Button>();
        button_NextScene.onClick.AddListener(() => AsyncSceneChange().Forget());
        text_Description = transform.Find("TitleUI/Panel_Progress/Text_Progress").GetComponent<TextMeshProUGUI>();
        InitManager().Forget();
    }
    async UniTask InitManager()
    {
        text_Description.text = "Waiting ... ";

        DataManager.Instance.InitAddressableSystem();
        await UniTask.WaitUntil(() => DataManager.AddressableSystem.IsLoad);

        DataManager.Instance.Initialize();
        await UniTask.WaitUntil(() => DataManager.Instance.IsLoad);

        LocalizingManager.Instance.Initialize();
        await UniTask.WaitUntil(() => LocalizingManager.Instance.IsLoad);

        //UIManager.Instance.Initialize();
        //await UniTask.WaitUntil(() => UIManager.Instance.IsLoad);
    }

    private async UniTask AsyncSceneChange()
    {
        try
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("MainScene").ToUniTask();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene: {e.Message}");
        }
        finally
        {
            GameObject.Find("MainScene").GetComponent<MainScene>().StartScene();
            DestroyImmediate(gameObject);
        }
    }
}
