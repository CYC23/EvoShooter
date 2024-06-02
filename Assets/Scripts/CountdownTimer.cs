using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 10; // 倒數計時的秒數
    public TextMeshProUGUI countdownText; // 參考倒數計時文本UI元件

    // 添加一個事件來通知計時結束
    public event System.Action OnTimerEnd;

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        else if (timeRemaining <= 0 && OnTimerEnd != null)
        {
            timeRemaining = 0;
            DisplayTime(timeRemaining);
            OnTimerEnd.Invoke(); // 通知計時結束
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);//+ 
        //Console.WriteLine(OnTimerEnd.ToString());
    }
}

