using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    [Header("Scene")]
    public TilemapVisualizer tilemapVisualizer;
    public GameObject grid;

    [Header("Canvas")]
    public GameObject mainMenuCanvas;

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Gluttons")]
    public GameObject gluttonPrefab;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(state);
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("<color=orange>[GAME STATE] : " + newState + "</color>");
        state = newState;

        switch (newState)
        {
            case GameState.INTRO:
                HandleIntro();
                break;
            case GameState.MAIN_MENU:
                HandleMainMenu();
                break;
            case GameState.LOADING:
                HandleLoading();
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

    private void HandleIntro()
    {
        throw new NotImplementedException();
    }

    private void HandleMainMenu()
    {
        throw new NotImplementedException();
    }

    private void HandleLoading()
    {
        Debug.Log("Loading game ...");

        //STEP 1 : Creating tile maps and assigning it to TileMapVisualizer

        //STEP  2 : Generating dungeon and kitchen

        //STEP 3 : Placing player at spawn

        //STEP 4 : Placing gluttons on map

        //STEP 5 : Placing food, fake food and objects on map

        //STEP 6 : Placing gluttons burrows (terriers)

        //STEP 8 : Init player and gluttons stats (use default values of prefab)

        //STEP 9 : Init receips and countdown 

        UpdateGameState(GameState.PLAYING); //When loading complete, we start the game !
    }

    private void HandlePlayMode()
    {
        Debug.Log("Playing !");
        throw new NotImplementedException();
    }

    private void HandlePauseMode()
    {
        throw new NotImplementedException();
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }

}
public enum GameState
{
    INTRO,
    MAIN_MENU,
    LOADING,
    PLAYING,
    PAUSE,
    GAME_OVER
}


