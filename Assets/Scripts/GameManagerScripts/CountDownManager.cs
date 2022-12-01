using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CountDownManager
{
    public GameObject countDownDisplayerText;
    private bool timerIsRunning;
    private float timeRemaining;
    private string timeText;

    public CountDownManager(float _totalTimeInSeconds, GameObject _countDownDiplayerText)
    {
        this.timeRemaining = _totalTimeInSeconds;
        this.timerIsRunning = false;
        this.countDownDisplayerText = _countDownDiplayerText;
    }

    public void StartCountDown()
    {
        timerIsRunning = true;
    }

    public void StopCountDown()
    {
        timerIsRunning = false;
    }

    public void UpdateCountDown()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                GameManager.Instance.UpdateGameState(GameState.GAME_OVER);
            }

            DisplayTime(timeRemaining);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        countDownDisplayerText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
