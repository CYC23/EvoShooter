using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float SpawnTime = 1f;
    [SerializeField] private GameObject[] enemyPrefab;
    private float spawnTimer = 0;
    private string currentSceneName;
    private HumanPlaySceneManager humanPlaySceneManager;
    private AgentPlaySceneManager agentPlaySceneManager;

    private void Awake()
    {
        // 在Awake方法中调用Unity API
        currentSceneName = SceneManager.GetActiveScene().name;
        humanPlaySceneManager = FindObjectOfType<HumanPlaySceneManager>();
        agentPlaySceneManager = FindObjectOfType<AgentPlaySceneManager>();
    }

    private void Start()
    {
        int random = Random.Range(0, enemyPrefab.Length);
        GameObject enemyToSpawn = enemyPrefab[random];
        Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        // 检查GameState是否已初始化
        if (humanPlaySceneManager != null && humanPlaySceneManager.GameState == HumanPlaySceneManager.GameStates.End ||
            agentPlaySceneManager != null && agentPlaySceneManager.GameState == AgentPlaySceneManager.GameStates.End)
        {
            // 如果游戏结束或者游戏状态不是 Running，则停止生成敌人
            return;
        }

        if (spawnTimer > SpawnTime)
        {
            spawnTimer = 0;
            int random = Random.Range(0, enemyPrefab.Length);
            GameObject enemyToSpawn = enemyPrefab[random];
            Instantiate(enemyToSpawn, transform.position, Quaternion.identity);
        }
        spawnTimer += Time.deltaTime;
    }
}

