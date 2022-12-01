using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownManager
{
    public GameObject countDown;
    private bool timerIsRunning;
    private float timeRemaining;

    public CountDownManager(float _totalTimeInSeconds)
    {
        this.timeRemaining = _totalTimeInSeconds;
        this.timerIsRunning = false;
    }

    public void StartCountDown()
    {
        Debug.Log("Countdown running");
        timerIsRunning = true;
    }

    public void StopCountDown()
    {
        Debug.Log("Countdown stopped");
        timerIsRunning = false;
    }

    public void UpdateCountDown()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                Debug.Log(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                GameManager.Instance.UpdateGameState(GameState.GAME_OVER);
            }
        }
    }
}
