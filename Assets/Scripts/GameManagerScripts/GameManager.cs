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

    private void Start()
    {
        UpdateGameState(GameState.INTRO);
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
            case GameState.PLAYING:
                HandlePlayMode();
                break;
            case GameState.PAUSE:
                HandlePauseMode();
                break;
            case GameState.GAME_OVER:
                HandleGameOver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged(newState);
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }

    private void HandlePauseMode()
    {
        throw new NotImplementedException();
    }

    private void HandlePlayMode()
    {
        throw new NotImplementedException();
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
    GAME_OVER
}
