using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

[System.Serializable]
public class Preference
{
    public bool BGM = true;
    public bool SFX = true;
    public bool Alram = true;
    public bool JoyStick = true;
    public bool Vibration = true;
    public bool Effect = true;

    //public bool ConsentedGDPR = false;
    public eLanguage Language = eLanguage.KR;
    public Preference ShallowCopy()
    {
        return (Preference)MemberwiseClone();
    }
}
public class RuntimePreference : TSingletonMono<RuntimePreference>
{
    #region Fields
    [SerializeField]
    Preference preference;
    public static Preference Preference => Instance.preference;
    #endregion
    public bool GetPreference(ePreference preferenceType)
    {
        return preferenceType switch
        {
            ePreference.BGM => preference.BGM,
            ePreference.SFX => preference.SFX,
            ePreference.Alram => preference.Alram,
            ePreference.JoyStick => preference.JoyStick,
            ePreference.Vibration => preference.Vibration,
            ePreference.Effect => preference.Effect,
            _ =>false,
        };
    }
    public void TogglePreference(ePreference preferenceType)
    {
        switch (preferenceType)
        {
            case ePreference.BGM:
                preference.BGM = !preference.BGM; break;
            case ePreference.SFX:
                preference.SFX = !preference.SFX; break;
            case ePreference.Alram:
                preference.Alram = !preference.Alram; break;
            case ePreference.JoyStick:
                preference.JoyStick = !preference.JoyStick; break;
            case ePreference.Vibration:
                preference.Vibration = !preference.Vibration; break;
            case ePreference.Effect:
                preference.Effect = !preference.Effect; break;
        }
    }
    protected override void OnInitialize()
    {

        if (Directory.Exists(GameConst.CacheDirectoryPath)==false)
            Directory.CreateDirectory(GameConst.CacheDirectoryPath);

        if (LoadPreference()==false)
        {
            DefaultSetting();
            SavePreference();
        }
        IsLoad = true;
    }

    //언어 추가 시 LocalizingManager에서 추가 후 DefaultSetting 메소드에서도 추가
    void DefaultSetting()
    {
        eLanguage language = eLanguage.EN;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
                language = eLanguage.KR;
                break;
        }
        preference = new Preference()
        {
            BGM = true,
            SFX = true,
            Alram = true,
            JoyStick = true,
            Vibration = true,
            Effect = true,
            Language = language,
        };
    }
    public bool LoadPreference()
    {
        if (File.Exists(GameConst.CacheDirectoryPath + "Preference.json")==false)
            return false;

        FileStream fs = new FileStream(GameConst.CacheDirectoryPath + "Preference.json", FileMode.Open);
        byte[] byteArr = new byte[fs.Length];
        try {
            fs.Read(byteArr, 0, (int)fs.Length);
        }
        catch(IOException e) {
            Debug.LogWarning(e);
            fs.Close();
            return false;
        }
        string str = null;
        try {
            str = Encoding.UTF8.GetString(byteArr);
        }
        catch {
            Debug.LogWarning("Encoding Error");
            fs.Close();
            return false;
        }
        Preference preference = null;
        try {
            preference = JsonConvert.DeserializeObject<Preference>(str);
        }
        catch(JsonException e) {
            fs.Close();
            return false;
        }
        if (preference != null)
        {
            this.preference = preference;
        }
        fs.Close();
        return true;
    }
    public void SavePreference()
    {
        FileStream fs = new FileStream(GameConst.CacheDirectoryPath + "Preference.json", FileMode.Create);
        string json = JsonConvert.SerializeObject(preference, Formatting.None);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);
        fs.Write(byteArray, 0, byteArray.Length);
        fs.Close();
    }
}
