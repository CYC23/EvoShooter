using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] private float SpawnTime = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    private float spawnTimer = 0;

    private void Start() {
        int random = Random.Range(0, enemyPrefab.Length);
        GameObject enemyToSpawn = enemyPrefab[random];
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }
    private void Update() {
        HumanPlaySceneManager manager = FindObjectOfType<HumanPlaySceneManager>();
        if (manager.GameState != HumanPlaySceneManager.GameStates.Running)
        {
            // 如果游戏结束或者游戏状态不是 Running，则停止生成敌人
            return;
        }
        if (spawnTimer > SpawnTime) {
            spawnTimer = 0;
            int random = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[random];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
        spawnTimer += Time.deltaTime;
    }
}
