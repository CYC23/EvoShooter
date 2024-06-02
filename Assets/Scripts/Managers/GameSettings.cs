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
        AgentResultCount();
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
        AgentResultCount();
        string json = JsonUtility.ToJson(options);
        File.WriteAllText(SETTINGS_PATH, json);
        Debug.Log("Settings saved to: " + SETTINGS_PATH);
    }


    public static void AgentResultCount()
    {
        options.Agent_R_HealthPoint = options.Agent_HealthPoint + options.Ability_HealthPoint * options.Agent_add_HealthPoint;
        options.Agent_R_AttackPoint = options.Agent_AttackPoint + options.Ability_AttackPoint * options.Agent_add_AttackPoint;
        options.Agent_R_FireRate = options.Agent_FireRate + options.Ability_FireRate * options.Agent_add_FireRate;
        options.Agent_R_MagazineSize = options.Agent_MagazineSize + options.Ability_MagazineSize * options.Agent_add_MagazineSize;
        options.Agent_R_MoveSpeed = options.Agent_MoveSpeed + options.Ability_MoveSpeed * options.Agent_add_MoveSpeed;
        options.Agent_R_RotateSpeed = options.Agent_RotateSpeed + options.Ability_RotateSpeed * options.Agent_add_RotateSpeed;
        options.Agent_R_ViewDistance = options.Agent_ViewDistance + options.Ability_ViewDistance * options.Agent_add_ViewDistance;
        options.Agent_R_BulletSpeed = options.Agent_BulletSpeed + options.Ability_BulletSpeed * options.Agent_add_BulletSpeed;


    }
}