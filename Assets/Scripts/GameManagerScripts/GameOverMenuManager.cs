using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuManager : MonoBehaviour
{
    public GameObject gameOverMenu;

    private void Awake()
    {
        GameManager.OnGameStateChanged += OnGameStateChanged; //When Game State change, call this method (Subscribe)
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= OnGameStateChanged; //Unsubscribe

    }
    private void OnGameStateChanged(GameState state)
    {
        gameOverMenu.SetActive(state == GameState.GAME_OVER); // Display the main menu if game state is MAIN_MENU  
    }

    public void OnReplayClick()
    {
        GameManager.Instance.UpdateGameState(GameState.LOADING);
    }

    public void OnQuitClick()
    {
        GameManager.Instance.UpdateGameState(GameState.MAIN_MENU);
    }
}
