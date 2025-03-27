using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    protected abstract void OnStartScene();
    protected abstract void OnStopScene();

    public void StartScene()
    {
        OnStartScene();
        DontDestroyOnLoad(this);
    }
    public void StopScene()
    {
        OnStopScene();
        DestroyImmediate(gameObject);
    }
}
