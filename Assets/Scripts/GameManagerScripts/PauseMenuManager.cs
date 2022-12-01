using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

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
        pauseMenu.SetActive(state == GameState.PAUSE); // Display the main menu if game state is MAIN_MENU  
    }

    public void OnContinueClick()
    {
        GameManager.Instance.UpdateGameState(GameState.PLAYING);
    }

    public void OnQuitClick()
    {
        GameManager.Instance.UpdateGameState(GameState.MAIN_MENU);
    }
}
