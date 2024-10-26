using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LocalizingManager : TSingletonMono<LocalizingManager>,ISubject<eLanguage>
{
    private List<IObserver<eLanguage>> observers = new List<IObserver<eLanguage>>();
    public static bool IsBeingDestroyed = false;
    public eLanguage CurrentLanguage
    {
        get => RuntimePreference.Preference.Language;
        set 
        {
            if (RuntimePreference.Preference.Language == value) 
                return;

                RuntimePreference.Preference.Language = value;
                OnLanguageChanged();         
        }
    }

    #region AbstractMethod
    protected override void OnInitialize()
    {
        IsLoad = true;
    }
    private void OnApplicationQuit()
    {
        IsBeingDestroyed = true;
    }

    #endregion

    #region Localize Method
    public string GetLocalizing(int key)
    {
        if (DataManager.LocalizingTable[key] != null)
            return DataManager.LocalizingTable[key];

        return "Unfound table";
    }
    public string GetLocalizing(int key, params object[] parsingParameters)
    {
        return string.Format(GetLocalizing(key), parsingParameters);
    }  
    void OnLanguageChanged()
    {
        Notify(RuntimePreference.Preference.Language);
    }
    #endregion  

    #region ISubject Method
    public void AddObserver(IObserver<eLanguage> observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }
    public void RemoveObserver(IObserver<eLanguage> observer)
    {
        if (observers.Contains(observer))
            observers.Remove(observer);
    }
    public void Notify(eLanguage value)
    {
        if (observers.Count < 1)
            return;

        foreach (var observer in observers)
            observer.OnNotify(value);
        //이건 목록 복사를 고민해보자 .
        //var observersCopy = new List<IObserver<eLanguage>>(observers);
        //foreach (var observer in handlersCopy)
        //{
        //    observer.OnNotify(value);
        //}
    }
    #endregion
}
