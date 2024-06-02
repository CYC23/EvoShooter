using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using static HumanPlaySceneManager;
using UnityEngine.SocialPlatforms.Impl;


public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // 倒數計時的秒數
    public TextMeshProUGUI countdownText; // 參考倒數計時文本UI元件
    public enum States
    {
        Idle, Run, Stop 
    }
    public States TimerState;

    // 添加一個事件來通知計時結束
    //public event System.Action OnTimerEnd;

    private void Awake()
    {
        TimerState = States.Idle;
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
            else if (timeRemaining <= 0)// && OnTimerEnd != null
            {
                Debug.Log("time stop!");
                TimerState = States.Stop;
                timeRemaining = 0;
                DisplayTime(timeRemaining);
                // 通知計時結束OnTimerEnd.Invoke();
            }
        }
        
    }

    public void Start()
    {
        TimerState = States.Run;
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);//+ 
    }
}

