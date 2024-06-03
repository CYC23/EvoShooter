using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentPlaySceneManager : MonoBehaviour {

    public enum GameStates {
        Pause, Running, End
    }
    public static AgentPlaySceneManager manager;
    public GameObject gameOverScreen;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public int score;
    public GameStates GameState;
    public SaveData data;
    public CountdownTimer GameTime;

    private int _Currentscore;
    private void Awake() {
        manager = this;
        SystemIO.Initialize();
        data = new SaveData(0,0,0,0);
        score = 0;
        GameSettings.LoadSettings();

        //gameOverScreen.SetActive(false);

    }
    private void Start() {
        GameState = GameStates.Running;
        //GameTime.Start();
    }
    private void Update()
    {
        
        if(GameTime.TimerState == CountdownTimer.States.Stop && GameState == GameStates.Running)
        {
            GameOver();
        }
        
        //Debug.Log(manager);
    }
    public void GameOver() {
        GameState = GameStates.End;
        gameOverScreen.SetActive(true);
        scoreText.text = $"Score: {score}";
        SystemIO.SaveHighscore(data, score);
        //Debug.Log(data.highScore);
        highScoreText.text = $"High Score : {data.highScore}";
    }
    public void ReplayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangeToMenuScene() {
        SceneManager.LoadScene("Menu");
    }
    public void IncreaseScore(int amount) {

        if (GameState == GameStates.Running)
        {
            score += amount;
            _Currentscore = score;
        }
        
    }

}


