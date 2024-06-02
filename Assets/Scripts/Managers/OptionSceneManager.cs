using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionSceneManager : MonoBehaviour {
    // Agent Status
    public TMP_InputField Agent_HealthPoint;
    public TMP_InputField Agent_AttackPoint;
    public TMP_InputField Agent_FireRate;
    public TMP_InputField Agent_MagazineSize;
    public TMP_InputField Agent_MoveSpeed;
    public TMP_InputField Agent_RotateSpeed;
    public TMP_InputField Agent_ViewDistance;
    public TMP_InputField Agent_BulletSpeed;
    // Agent add
    public TMP_InputField Agent_add_HealthPoint;
    public TMP_InputField Agent_add_AttackPoint;
    public TMP_InputField Agent_add_FireRate;
    public TMP_InputField Agent_add_MagazineSize;
    public TMP_InputField Agent_add_MoveSpeed;
    public TMP_InputField Agent_add_RotateSpeed;
    public TMP_InputField Agent_add_ViewDistance;
    public TMP_InputField Agent_add_BulletSpeed;
    // Ability Ratio
    public TMP_InputField Ability_HealthPoint;
    public TMP_InputField Ability_AttackPoint;
    public TMP_InputField Ability_FireRate;
    public TMP_InputField Ability_MagazineSize;
    public TMP_InputField Ability_MoveSpeed;
    public TMP_InputField Ability_RotateSpeed;
    public TMP_InputField Ability_ViewDistance;
    public TMP_InputField Ability_BulletSpeed;
    // Agent result
    public TMP_InputField Agent_R_HealthPoint;
    public TMP_InputField Agent_R_AttackPoint;
    public TMP_InputField Agent_R_FireRate;
    public TMP_InputField Agent_R_MagazineSize;
    public TMP_InputField Agent_R_MoveSpeed;
    public TMP_InputField Agent_R_RotateSpeed;
    public TMP_InputField Agent_R_ViewDistance;
    public TMP_InputField Agent_R_BulletSpeed;
    // Enemy Score
    // Spawn Rate
    public TMP_InputField Score_Zombie;
    public TMP_InputField Score_Spitter;
    public TMP_InputField Score_Tank;
    public TMP_InputField Score_Charger;
    // Spawn Rate
    public TMP_InputField SpawnRate_Zombie;
    public TMP_InputField SpawnRate_Spitter;
    public TMP_InputField SpawnRate_Tank;
    public TMP_InputField SpawnRate_Charger;
    // Zombie Status
    public TMP_InputField Zombie_HealthPoint;
    public TMP_InputField Zombie_AttackPoint;
    public TMP_InputField Zombie_MoveSpeed;
    public TMP_InputField Zombie_RotateSpeed;
    // Tank Status
    public TMP_InputField Tank_HealthPoint;
    public TMP_InputField Tank_AttackPoint;
    public TMP_InputField Tank_MoveSpeed;
    public TMP_InputField Tank_RotateSpeed;
    public TMP_InputField Tank_ViewDistance;
    // Spitter Status
    public TMP_InputField Spitter_HealthPoint;
    public TMP_InputField Spitter_AttackPoint;
    public TMP_InputField Spitter_MoveSpeed;
    public TMP_InputField Spitter_RotateSpeed;
    public TMP_InputField Spitter_FireRate;
    public TMP_InputField Spitter_ViewDistance;
    // Charger Status
    public TMP_InputField Charger_HealthPoint;
    public TMP_InputField Charger_AttackPoint;
    public TMP_InputField Charger_MoveSpeed;
    public TMP_InputField Charger_RotateSpeed;
    public TMP_InputField Charger_ChargeSpeed;
    public TMP_InputField Charger_FireRate;
    public TMP_InputField Charger_ViewDistance;

    private void Awake() {
        LoadSettings();
    }

    public void ChangeToMenuScene() {
        SceneManager.LoadScene("Menu");
    }

    public void ApplySettings() {


        GameSettings.options.Agent_HealthPoint = int.Parse(Agent_HealthPoint.text);
        GameSettings.options.Agent_AttackPoint = int.Parse(Agent_AttackPoint.text);
        GameSettings.options.Agent_FireRate = float.Parse(Agent_FireRate.text);
        GameSettings.options.Agent_MagazineSize = int.Parse(Agent_MagazineSize.text);
        GameSettings.options.Agent_MoveSpeed = float.Parse(Agent_MoveSpeed.text);
        GameSettings.options.Agent_RotateSpeed = float.Parse(Agent_RotateSpeed.text);
        GameSettings.options.Agent_ViewDistance = float.Parse(Agent_ViewDistance.text);
        GameSettings.options.Agent_BulletSpeed = float.Parse(Agent_BulletSpeed.text);

        GameSettings.options.Agent_add_HealthPoint = int.Parse(Agent_add_HealthPoint.text);
        GameSettings.options.Agent_add_AttackPoint = int.Parse(Agent_add_AttackPoint.text);
        GameSettings.options.Agent_add_FireRate = float.Parse(Agent_add_FireRate.text);
        GameSettings.options.Agent_add_MagazineSize = int.Parse(Agent_add_MagazineSize.text);
        GameSettings.options.Agent_add_MoveSpeed = float.Parse(Agent_add_MoveSpeed.text);
        GameSettings.options.Agent_add_RotateSpeed = float.Parse(Agent_add_RotateSpeed.text);
        GameSettings.options.Agent_add_ViewDistance = float.Parse(Agent_add_ViewDistance.text);
        GameSettings.options.Agent_add_BulletSpeed = float.Parse(Agent_add_BulletSpeed.text);

        GameSettings.options.Ability_HealthPoint = int.Parse(Ability_HealthPoint.text);
        GameSettings.options.Ability_AttackPoint = int.Parse(Ability_AttackPoint.text);
        GameSettings.options.Ability_FireRate = float.Parse(Ability_FireRate.text);
        GameSettings.options.Ability_MagazineSize = int.Parse(Ability_MagazineSize.text);
        GameSettings.options.Ability_MoveSpeed = float.Parse(Ability_MoveSpeed.text);
        GameSettings.options.Ability_RotateSpeed = float.Parse(Ability_RotateSpeed.text);
        GameSettings.options.Ability_ViewDistance = float.Parse(Ability_ViewDistance.text);
        GameSettings.options.Ability_BulletSpeed = float.Parse(Ability_BulletSpeed.text);

        //GameSettings.options.Agent_R_HealthPoint = int.Parse(Agent_R_HealthPoint.text);
        //GameSettings.options.Agent_R_AttackPoint = int.Parse(Agent_R_AttackPoint.text);
        //GameSettings.options.Agent_R_FireRate = float.Parse(Agent_R_FireRate.text);
        //GameSettings.options.Agent_R_MagazineSize = int.Parse(Agent_R_MagazineSize.text);
        //GameSettings.options.Agent_R_MoveSpeed = float.Parse(Agent_R_MoveSpeed.text);
        //GameSettings.options.Agent_R_RotateSpeed = float.Parse(Agent_R_RotateSpeed.text);
        //GameSettings.options.Agent_R_ViewDistance = float.Parse(Agent_R_ViewDistance.text);
        //GameSettings.options.Agent_R_BulletSpeed = float.Parse(Agent_R_BulletSpeed.text);

        GameSettings.options.SpawnRate_Zombie = int.Parse(SpawnRate_Zombie.text);
        GameSettings.options.SpawnRate_Spitter = int.Parse(SpawnRate_Spitter.text);
        GameSettings.options.SpawnRate_Tank = int.Parse(SpawnRate_Tank.text);
        GameSettings.options.SpawnRate_Charger = int.Parse(SpawnRate_Charger.text);

        GameSettings.options.Score_Zombie = int.Parse(Score_Zombie.text);
        GameSettings.options.Score_Spitter = int.Parse(Score_Spitter.text);
        GameSettings.options.Score_Tank = int.Parse(Score_Tank.text);
        GameSettings.options.Score_Charger = int.Parse(Score_Charger.text);

        GameSettings.options.Zombie_HealthPoint = int.Parse(Zombie_HealthPoint.text);
        GameSettings.options.Zombie_AttackPoint = int.Parse(Zombie_AttackPoint.text);
        GameSettings.options.Zombie_MoveSpeed = float.Parse(Zombie_MoveSpeed.text);
        GameSettings.options.Zombie_RotateSpeed = float.Parse(Zombie_RotateSpeed.text);

        GameSettings.options.Spitter_HealthPoint = int.Parse(Spitter_HealthPoint.text);
        GameSettings.options.Spitter_AttackPoint = int.Parse(Spitter_AttackPoint.text);
        GameSettings.options.Spitter_MoveSpeed = float.Parse(Spitter_MoveSpeed.text);
        GameSettings.options.Spitter_RotateSpeed = float.Parse(Spitter_RotateSpeed.text);
        GameSettings.options.Spitter_FireRate = float.Parse(Spitter_FireRate.text);
        GameSettings.options.Spitter_ViewDistance = float.Parse(Spitter_ViewDistance.text);

        GameSettings.options.Tank_HealthPoint = int.Parse(Tank_HealthPoint.text);
        GameSettings.options.Tank_AttackPoint = int.Parse(Tank_AttackPoint.text);
        GameSettings.options.Tank_MoveSpeed = float.Parse(Tank_MoveSpeed.text);
        GameSettings.options.Tank_RotateSpeed = float.Parse(Tank_RotateSpeed.text);
        GameSettings.options.Tank_ViewDistance = float.Parse(Tank_ViewDistance.text);

        GameSettings.options.Charger_HealthPoint = int.Parse(Charger_HealthPoint.text);
        GameSettings.options.Charger_AttackPoint = int.Parse(Charger_AttackPoint.text);
        GameSettings.options.Charger_MoveSpeed = float.Parse(Charger_MoveSpeed.text);
        GameSettings.options.Charger_RotateSpeed = float.Parse(Charger_RotateSpeed.text);
        GameSettings.options.Charger_ChargeSpeed = float.Parse(Charger_ChargeSpeed.text);
        GameSettings.options.Charger_FireRate = float.Parse(Charger_FireRate.text);
        GameSettings.options.Charger_ViewDistance = float.Parse(Charger_ViewDistance.text);

        GameSettings.SaveSettings();
    }
    public void LoadSettings() {
        GameSettings.LoadSettings();
        Agent_HealthPoint.text = GameSettings.options.Agent_HealthPoint.ToString();
        Agent_AttackPoint.text = GameSettings.options.Agent_AttackPoint.ToString();
        Agent_FireRate.text = GameSettings.options.Agent_FireRate.ToString();
        Agent_MagazineSize.text = GameSettings.options.Agent_MagazineSize.ToString();
        Agent_MoveSpeed.text = GameSettings.options.Agent_MoveSpeed.ToString();
        Agent_RotateSpeed.text = GameSettings.options.Agent_RotateSpeed.ToString();
        Agent_ViewDistance.text = GameSettings.options.Agent_ViewDistance.ToString();
        Agent_BulletSpeed.text = GameSettings.options.Agent_BulletSpeed.ToString();

        Agent_add_HealthPoint.text = GameSettings.options.Agent_add_HealthPoint.ToString();
        Agent_add_AttackPoint.text = GameSettings.options.Agent_add_AttackPoint.ToString();
        Agent_add_FireRate.text = GameSettings.options.Agent_add_FireRate.ToString();
        Agent_add_MagazineSize.text = GameSettings.options.Agent_add_MagazineSize.ToString();
        Agent_add_MoveSpeed.text = GameSettings.options.Agent_add_MoveSpeed.ToString();
        Agent_add_RotateSpeed.text = GameSettings.options.Agent_add_RotateSpeed.ToString();
        Agent_add_ViewDistance.text = GameSettings.options.Agent_add_ViewDistance.ToString();
        Agent_add_BulletSpeed.text = GameSettings.options.Agent_add_BulletSpeed.ToString();

        Ability_HealthPoint.text = GameSettings.options.Ability_HealthPoint.ToString();
        Ability_AttackPoint.text = GameSettings.options.Ability_AttackPoint.ToString();
        Ability_FireRate.text = GameSettings.options.Ability_FireRate.ToString();
        Ability_MagazineSize.text = GameSettings.options.Ability_MagazineSize.ToString();
        Ability_MoveSpeed.text = GameSettings.options.Ability_MoveSpeed.ToString();
        Ability_RotateSpeed.text = GameSettings.options.Ability_RotateSpeed.ToString();
        Ability_ViewDistance.text = GameSettings.options.Ability_ViewDistance.ToString();
        Ability_BulletSpeed.text = GameSettings.options.Ability_BulletSpeed.ToString();

        Agent_R_HealthPoint.text = GameSettings.options.Agent_R_HealthPoint.ToString();
        Agent_R_AttackPoint.text = GameSettings.options.Agent_R_AttackPoint.ToString();
        Agent_R_FireRate.text = GameSettings.options.Agent_R_FireRate.ToString();
        Agent_R_MagazineSize.text = GameSettings.options.Agent_R_MagazineSize.ToString();
        Agent_R_MoveSpeed.text = GameSettings.options.Agent_R_MoveSpeed.ToString();
        Agent_R_RotateSpeed.text = GameSettings.options.Agent_R_RotateSpeed.ToString();
        Agent_R_ViewDistance.text = GameSettings.options.Agent_R_ViewDistance.ToString();
        Agent_R_BulletSpeed.text = GameSettings.options.Agent_R_BulletSpeed.ToString();

        SpawnRate_Zombie.text = GameSettings.options.SpawnRate_Zombie.ToString();
        SpawnRate_Tank.text = GameSettings.options.SpawnRate_Tank.ToString();
        SpawnRate_Spitter.text = GameSettings.options.SpawnRate_Spitter.ToString();
        SpawnRate_Charger.text = GameSettings.options.SpawnRate_Charger.ToString();

        Score_Zombie.text = GameSettings.options  .Score_Zombie.ToString();
        Score_Tank.text = GameSettings.options    .Score_Tank.ToString();
        Score_Spitter.text = GameSettings.options .Score_Spitter.ToString();
        Score_Charger.text = GameSettings.options .Score_Charger.ToString();

        Zombie_HealthPoint.text = GameSettings.options.Zombie_HealthPoint.ToString();
        Zombie_AttackPoint.text = GameSettings.options.Zombie_AttackPoint.ToString();
        Zombie_MoveSpeed.text = GameSettings.options.Zombie_MoveSpeed.ToString();
        Zombie_RotateSpeed.text = GameSettings.options.Zombie_RotateSpeed.ToString();

        Spitter_HealthPoint.text = GameSettings.options.Spitter_HealthPoint.ToString();
        Spitter_AttackPoint.text = GameSettings.options.Spitter_AttackPoint.ToString();
        Spitter_MoveSpeed.text = GameSettings.options.Spitter_MoveSpeed.ToString();
        Spitter_RotateSpeed.text = GameSettings.options.Spitter_RotateSpeed.ToString();
        Spitter_FireRate.text = GameSettings.options.Spitter_FireRate.ToString();
        Spitter_ViewDistance.text = GameSettings.options.Spitter_ViewDistance.ToString();

        Tank_HealthPoint.text = GameSettings.options.Tank_HealthPoint.ToString();
        Tank_AttackPoint.text = GameSettings.options.Tank_AttackPoint.ToString();
        Tank_MoveSpeed.text = GameSettings.options.Tank_MoveSpeed.ToString();
        Tank_RotateSpeed.text = GameSettings.options.Tank_RotateSpeed.ToString();
        Tank_ViewDistance.text = GameSettings.options.Tank_ViewDistance.ToString();

        Charger_HealthPoint.text = GameSettings.options.Charger_HealthPoint.ToString();
        Charger_AttackPoint.text = GameSettings.options.Charger_AttackPoint.ToString();
        Charger_MoveSpeed.text = GameSettings.options.Charger_MoveSpeed.ToString();
        Charger_RotateSpeed.text = GameSettings.options.Charger_RotateSpeed.ToString();
        Charger_ChargeSpeed.text = GameSettings.options.Charger_ChargeSpeed.ToString();
        Charger_FireRate.text = GameSettings.options.Charger_FireRate.ToString();
        Charger_ViewDistance.text = GameSettings.options.Charger_ViewDistance.ToString();   
    }


}

