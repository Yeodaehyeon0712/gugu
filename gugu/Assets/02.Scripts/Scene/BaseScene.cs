using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class BaseScene : MonoBehaviour
{
    public abstract void StartScene();
    public virtual void StopScene()
    {
        DestroyImmediate(gameObject);
    }
}
