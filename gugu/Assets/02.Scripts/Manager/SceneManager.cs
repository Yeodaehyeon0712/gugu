using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : TSingletonMono<SceneManager>
{
    protected override void OnInitialize()
    {
        IsLoad = true;
    }

    public async UniTask AsyncSceneChange<T>() where T : BaseScene
    {
        try
        {
            await UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(typeof(T).Name).ToUniTask();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene: {e.Message}");
        }
        finally
        {
            await UniTask.Yield();
            GameObject.Find(typeof(T).Name).GetComponent<T>().StartScene();
            DestroyImmediate(gameObject);
        }
    }
}
