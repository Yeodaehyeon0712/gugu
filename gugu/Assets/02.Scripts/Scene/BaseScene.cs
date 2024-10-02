using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public abstract void StartScene();
    protected async UniTask AsyncSceneChange<T>() where T : BaseScene
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
