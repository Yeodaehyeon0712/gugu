using UnityEngine;
using TMPro;
[RequireComponent(typeof(TextMeshProUGUI))]

public class LocalizingText : MonoBehaviour,IObserver<eLanguage>
{
    int index=0;
    object[] parsingParameters=null;
    TextMeshProUGUI text;

    #region UnityMethods
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void OnEnable()
    {
        LocalizingManager.Instance.AddObserver(this);
        SetLocalizingText();
    }
    void OnDisable()
    {
        if (LocalizingManager.IsBeingDestroyed) 
            return;
        LocalizingManager.Instance.RemoveObserver(this);
    }
    #endregion

    #region Localizing Method
    public void Init(int index,params object[]parsingParameters)
    {
        this.index = index;
        this.parsingParameters = parsingParameters;
        SetLocalizingText();
    }
    void SetLocalizingText()
    {
        if (parsingParameters == null)
            text.text = LocalizingManager.Instance.GetLocalizing(index);
        else
            text.text = LocalizingManager.Instance.GetLocalizing(index, parsingParameters);
    }
    #endregion

    #region Observer Method
    public void OnNotify(eLanguage value)
    {
        SetLocalizingText();
    }
    #endregion
}
