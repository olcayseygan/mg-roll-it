using UnityEngine;

public static class PlayerPrefsManager
{
    private const string UPDATED_AT_KEY = "UPDATED_AT";
    private const string SAVED_AT_KEY = "SAVED_AT";

    private static string AppendUsernameToKey(string key)
    {
        return PlayerPrefs.GetString("PLAYER_NAME", "") + "_" + key;
    }

    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        Update();
    }

    public static void DeleteKey(string key)
    {
        PlayerPrefs.DeleteKey(AppendUsernameToKey(key));
        Update();
    }

    public static float GetFloat(string key, float defaultValue = 0f)
    {
        return PlayerPrefs.GetFloat(AppendUsernameToKey(key), defaultValue);
    }

    public static int GetInt(string key, int defaultValue = 0)
    {
        return PlayerPrefs.GetInt(AppendUsernameToKey(key), defaultValue);
    }

    public static string GetString(string key, string defaultValue = "")
    {
        return PlayerPrefs.GetString(AppendUsernameToKey(key), defaultValue);
    }

    public static bool HasKey(string key)
    {
        return PlayerPrefs.HasKey(AppendUsernameToKey(key));
    }

    public static void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(AppendUsernameToKey(key), value);
        Update();
    }

    public static void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(AppendUsernameToKey(key), value);
        Update();
    }

    public static void SetString(string key, string value)
    {
        PlayerPrefs.SetString(AppendUsernameToKey(key), value);
        Update();
    }

    public static void Save()
    {
        PlayerPrefs.SetString(AppendUsernameToKey(SAVED_AT_KEY), System.DateTime.UtcNow.ToString());
        Update();
    }

    private static void Update()
    {
        PlayerPrefs.SetString(AppendUsernameToKey(UPDATED_AT_KEY), System.DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
    }

    public static bool IsDataIntegrityVerified()
    {
        var savedAt = PlayerPrefs.GetString(AppendUsernameToKey(SAVED_AT_KEY), "");
        var updatedAt = PlayerPrefs.GetString(AppendUsernameToKey(UPDATED_AT_KEY), "");
        if (string.IsNullOrEmpty(savedAt) || string.IsNullOrEmpty(updatedAt))
        {
            return false;
        }

        var savedAtDate = System.DateTime.Parse(savedAt);
        var updatedAtDate = System.DateTime.Parse(updatedAt);
        return updatedAtDate > savedAtDate;
    }
}
