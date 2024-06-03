using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // 倒數計時的秒數
    public TextMeshProUGUI countdownText; // 參考倒數計時文本UI元件
    public enum States
    {
        Idle, Run, Stop
    }
    public States TimerState;
    private string currentSceneName;

    private void Awake()
    {
        TimerState = States.Idle;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    public void Start()
    {
        TimerState = States.Run;

        // 添加调试日志
        if (countdownText == null)
        {
            Debug.LogError("CountdownText is not assigned!");
        }
    }

    void Update()
    {
        if (TimerState == States.Run)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else if (timeRemaining <= 0)
            {
                Debug.Log("time stop!");
                TimerState = States.Stop;
                timeRemaining = 0;
                DisplayTime(timeRemaining);
            }
        }

        if (currentSceneName == "HumanGame")
        {
            if (HumanPlaySceneManager.manager.GameState == HumanPlaySceneManager.GameStates.End)
            {
                TimerState = States.Stop;
            }
        }
        else if (currentSceneName == "AgentGame")
        {
            if (AgentPlaySceneManager.manager.GameState == AgentPlaySceneManager.GameStates.End)
            {
                TimerState = States.Stop;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        // 添加空值检查和调试日志
        if (countdownText == null)
        {
            Debug.LogError("CountdownText is not assigned!");
            return;
        }

        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
