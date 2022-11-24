using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    void Awake()
    {
        Instance = this;
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.INTRO:
                HandleIntro();
                break;
            case GameState.MAIN_MENU:
                HandleMainMenu();
                break;
        }
    }

    private void HandleMainMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleIntro()
    {
        throw new NotImplementedException();
    }
}

public enum GameState
{
    INTRO,
    MAIN_MENU,
    PLAYING,
    PAUSE,
    WIN,
    GAME_OVER
}
