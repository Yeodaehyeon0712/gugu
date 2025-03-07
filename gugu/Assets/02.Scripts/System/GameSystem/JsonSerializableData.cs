using UnityEngine;
using System.IO;
using System.Text;
using Newtonsoft.Json;

public abstract class JsonSerializableData<T> : TSingletonMono<JsonSerializableData<T>> where T:class
{
    #region Fields
    [SerializeField] T data;
    public static T Data => Instance.data;
    #endregion
    protected override void OnInitialize()
    {
        if (LoadData() == false)
        {
            SetDefaultValue();
            SaveData();
        }
        IsLoad = true;
    }

    #region Data Method
    protected abstract void SetDefaultValue();
    
    public bool LoadData()
    {
        if (File.Exists(GameConst.CacheDirectoryPath + $"{typeof(T).Name}.json") == false)
            return false;

        FileStream fs = new FileStream(GameConst.CacheDirectoryPath + $"{typeof(T).Name}.json", FileMode.Open);
        byte[] byteArr = new byte[fs.Length];
        try
        {
            fs.Read(byteArr, 0, (int)fs.Length);
        }
        catch (IOException e)
        {
            Debug.LogWarning(e);
            fs.Close();
            return false;
        }
        string str = null;
        try
        {
            str = Encoding.UTF8.GetString(byteArr);
        }
        catch
        {
            Debug.LogWarning("Encoding Error");
            fs.Close();
            return false;
        }
        T data = null;
        try
        {
            data = JsonConvert.DeserializeObject<T>(str);
        }
        catch (JsonException e)
        {
            Debug.LogWarning(e);
            fs.Close();
            return false;
        }
        if (data != null)
        {
            this.data = data;
        }
        fs.Close();
        return true;
    }
    public void SaveData()
    {
        FileStream fs = new FileStream(GameConst.CacheDirectoryPath + $"{typeof(T).Name}.json", FileMode.Create);
        string json = JsonConvert.SerializeObject(data, Formatting.None);
        byte[] byteArray = Encoding.UTF8.GetBytes(json);
        fs.Write(byteArray, 0, byteArray.Length);
        fs.Close();
    }
    #endregion

}
