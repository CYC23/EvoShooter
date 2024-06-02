using UnityEngine;
using System.IO;
using UnityEditor.ShaderGraph.Serialization;

public static class GameSettings {
    public static SettingData options = new();
    public readonly static string SETTINGS_FILENAME = "GamesSettings.json";
    public readonly static string SETTINGS_PATH = Path.Combine(Application.dataPath, SETTINGS_FILENAME);

    /*
    public static void LoadSettings() {
        if (PlayerPrefs.HasKey(SETTINGS_FILENAME)) {
            string json = PlayerPrefs.GetString(SETTINGS_FILENAME);
            options = JsonUtility.FromJson<SettingData>(json);
        }
        else {
            options = new();
        }
    }
    public static void SaveSettings() {
        string json = JsonUtility.ToJson(options);
        PlayerPrefs.SetString(SETTINGS_FILENAME, json);
        PlayerPrefs.Save();
    }
    */

    public static void LoadSettings()
    {
        if (File.Exists(SETTINGS_PATH))
        {
            Debug.Log("Settings loaded frpm: " + SETTINGS_PATH);
            string json = File.ReadAllText(SETTINGS_PATH);
            options = JsonUtility.FromJson<SettingData>(json);
        }
        else
        {
            Debug.Log("Created Settings to: " + SETTINGS_PATH);
            options = new();
        }
    }

    public static void SaveSettings()
    {
        string json = JsonUtility.ToJson(options);
        File.WriteAllText(SETTINGS_PATH, json);
        Debug.Log("Settings saved to: " + SETTINGS_PATH);
    }
}