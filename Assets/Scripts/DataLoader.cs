using UnityEngine;

public class DataLoader
{
    public enum PrefKeys
    {
        S_Data,
    }

    public void SaveToPrefs(PlayerDataSerializable data)
    {
        string result = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(PrefKeys.S_Data.ToString(), result);
    }

    public PlayerDataSerializable LoadFromPrefs()
    {
        string result = PlayerPrefs.GetString(PrefKeys.S_Data.ToString());

        PlayerDataSerializable data = JsonUtility.FromJson<PlayerDataSerializable>(result);

        return data;
    }
}
