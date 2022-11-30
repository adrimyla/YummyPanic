using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("Game Manager")]
    public static GameManager Instance;
    public GameState state;
    public static event Action<GameState> OnGameStateChanged;

    [Header("Scene")]
    public GameObject setup;
    public TilemapVisualizer tilemapVisualizer;
    public DungeonGenerator dungeon;

    [Header("Canvas")]
    public GameObject loadingScreen;

    [Header("Camera")]
    public CameraFollow camFollow;

    [Header("Player")]
    public GameObject playerPrefab;

    [Header("Gluttons")]
    public GameObject gluttonPrefab;
    [Range (2, 30)]    
    public int gluttonCount = 10;

    

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(state);
    }
    private void Update()
    {
        //Input to enter Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.PLAYING)
        {
            UpdateGameState(GameState.PAUSE);
        }

        //Input to exit Pause Menu
        else if (Input.GetKeyDown(KeyCode.Escape) && state == GameState.PAUSE)
        {
            UpdateGameState(GameState.PLAYING);
        }
    }

    public void UpdateGameState(GameState newState)
    {
        Debug.Log("<color=orange>[GAME STATE] : " + newState + "</color>");
        state = newState;

        OnGameStateChanged(newState);

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
            case GameState.EXIT_GAME:
                HandleExitGame();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
       
    }

    private void HandleExitGame()
    {
        Debug.Log("Exiting game");
        Application.Quit();
    }

    private void HandleIntro()
    {
        // Init game canvas
        loadingScreen.SetActive(false);

        UpdateGameState(GameState.MAIN_MENU);
    }

    private void HandleMainMenu()
    {
        Time.timeScale = 0;
    }

    private void HandlePlayMode()
    {
        Debug.Log("Playing !");
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
    }

    private void HandlePauseMode()
    {
        Time.timeScale = 0;
    }

    private void HandleGameOver()
    {
        throw new NotImplementedException();
    }

    private void HandleLoading()
    {
        Debug.Log("Loading game ...");

        loadingScreen.SetActive(true);

        //====== STEP 1 : Creating tile maps and assigning it to TileMapVisualizer ======

        //====== STEP  2 : Generating dungeon and kitchen ======
        
        Dungeon dg = dungeon.GenerateDungeon();

        //====== STEP 3 : Placing player at spawn ======         

        SpawningPlayer(dg);

        //====== STEP 4 : Placing gluttons on map ======

        SpawningGluttons(dg);

        //====== STEP 5 : Placing food, fake food and objects on map ======

        //====== STEP 6 : Placing gluttons burrows (terriers) ======

        //====== STEP 8 : Init player and gluttons stats (use default values of prefab) ======

        //====== STEP 9 : Init receips and countdown ======

        UpdateGameState(GameState.PLAYING); //When loading complete, we start the game !
    }

    private void SpawningGluttons(Dungeon dg)
    {   
        //Getting random position for gluttons
        for(int i = 0; i < gluttonCount; i++)
        {
            Vector2Int spawnPos2D = dg.freeFloorPositions[(Random.Range(0, dg.freeFloorPositions.Count))];
            dg.freeFloorPositions.Remove(spawnPos2D);

            Vector3 spawnPos3D = new Vector3((float)spawnPos2D.x, (float)spawnPos2D.y, 0);

            GameObject gluttonGO = Instantiate(gluttonPrefab, spawnPos3D, Quaternion.identity);
            gluttonGO.transform.parent = setup.transform;
        }
    }

    private void SpawningPlayer(Dungeon dg)
    {
        //Getting player start pos
        Vector2Int playerStartPos2D = dg.kitchen.playerPos;
        Vector3 playerStartPos3D = new Vector3((float)playerStartPos2D.x, (float)playerStartPos2D.y, 0);

        //Instantiate player
        GameObject playerGO = Instantiate(playerPrefab, playerStartPos3D, Quaternion.identity);
        playerGO.transform.parent = setup.transform;

        //Adding player as object to follow by camera
        camFollow.ObjectToFollow = playerGO.transform;
    }


}
public enum GameState
{
    INTRO,
    MAIN_MENU,
    LOADING,
    PLAYING,
    PAUSE,
    GAME_OVER,
    EXIT_GAME
}


