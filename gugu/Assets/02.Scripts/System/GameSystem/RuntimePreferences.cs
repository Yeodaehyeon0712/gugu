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
public class RuntimePreference : JsonSerializableData<Preference,RuntimePreference>
{
    #region Fields
    #endregion

    #region Preference Method
    //언어 추가 시 LocalizingManager에서 추가 후 DefaultSetting 메소드에서도 추가
    protected override void SetDefaultValue()
    {
        eLanguage language = eLanguage.EN;
        switch (Application.systemLanguage)
        {
            case SystemLanguage.Korean:
                language = eLanguage.KR;
                break;
        }
        data = new Preference()
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
    public bool GetPreference(ePreference preferenceType)
    {
        return preferenceType switch
        {
            ePreference.BGM => data.BGM,
            ePreference.SFX => data.SFX,
            ePreference.Alram => data.Alram,
            ePreference.JoyStick => data.JoyStick,
            ePreference.Vibration => data.Vibration,
            ePreference.Effect => data.Effect,
            _ =>false,
        };
    }
    public void TogglePreference(ePreference preferenceType)
    {
        switch (preferenceType)
        {
            case ePreference.BGM:
                data.BGM = !data.BGM; break;
            case ePreference.SFX:
                data.SFX = !data.SFX; break;
            case ePreference.Alram:
                data.Alram = !data.Alram; break;
            case ePreference.JoyStick:
                data.JoyStick = !data.JoyStick; break;
            case ePreference.Vibration:
                data.Vibration = !data.Vibration; break;
            case ePreference.Effect:
                data.Effect = !data.Effect; break;
        }
    }
    #endregion
}
