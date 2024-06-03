using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneManager : MonoBehaviour {

    void Start ()
    {
        // 获取构建设置中的场景数量
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        Debug.Log("Number of scenes in Build Settings: " + sceneCount);
    }

    public void ChangeToHumanPlayScene() {
        SceneManager.LoadScene("HumanGame");
    }

    public void ChangeToAgentPlayScene()
    {
        SceneManager.LoadScene("AgentGame");
    }
    public void ChangeToOptionsScene() {
        SceneManager.LoadScene("Options");
    }
    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
