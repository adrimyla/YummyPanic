using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

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

    [Header("Objects")]
    public int foodTotalCount = 10;
    public List<GameObject> food;
    public int fakeFoodTotalCount = 10;
    public List<GameObject> fakeFood;
    public int bonusTotalCount = 10;
    public List<GameObject> bonus;

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

        //====== STEP  1 : Generating dungeon and kitchen ======
        
        Dungeon dg = dungeon.GenerateDungeon();

        //====== STEP 2 : Placing player at spawn ======         

        SpawningPlayer(dg);

        //====== STEP 3 : Placing gluttons on map ======

        GameObject gluttonsContainerGO = Instantiate(new GameObject(), setup.transform);
        gluttonsContainerGO.name = "GLUTTONS";
        SpawningGluttons(dg, gluttonsContainerGO);

        //====== STEP 4 : Placing gluttons burrows (terriers) ======
        
        GameObject burrowsContainer = Instantiate(new GameObject(), setup.transform);
        burrowsContainer.name = "BURROWS";
        //SpawningGluttonsBurrows(burrowsContainer);

        //====== STEP 5 : Placing food, fake food and objects on map ======
        
        GameObject objectsContainerGO = Instantiate(new GameObject(), setup.transform);
        objectsContainerGO.name = "OBJECTS";
        SpawningFood(dg, objectsContainerGO);

        //====== STEP 6 : Init player and gluttons stats (use default values of prefab) ======

        //====== STEP 7 : Init receips and countdown ======

        UpdateGameState(GameState.PLAYING); //When loading complete, we start the game !
    }

    private void SpawningGluttonsBurrows(GameObject burrowsContainer)
    {
        throw new NotImplementedException();
    }

    private void SpawningFood(Dungeon dg, GameObject objectsContainerGO)
    {
        GameObject foodContainer = Instantiate(new GameObject(), objectsContainerGO.transform);
        foodContainer.name = "FOOD";
        for (int i = 0; i < foodTotalCount; i++)
        {
            //Getting a random spawn location            
            Vector3 spawnPos3D = FindFreeLocation(dg.freeFloorPositions);

            //Getting a random element in food
            GameObject foodPrefab = food[Random.Range(0, food.Count)];
            GameObject foodGO = Instantiate(foodPrefab, spawnPos3D, Quaternion.identity);
            foodGO.transform.parent = foodContainer.transform;

        }
    }

    private void SpawningGluttons(Dungeon dg, GameObject gluttonsContainerGO)
    {   
        for(int i = 0; i < gluttonCount; i++)
        {
            //Getting a random spawn location
            Vector3 spawnPos3D = FindFreeLocation(dg.freeFloorPositions);
            
            //Instantiate
            GameObject gluttonGO = Instantiate(gluttonPrefab, spawnPos3D, Quaternion.identity);
            gluttonGO.transform.parent = gluttonsContainerGO.transform;
        }
    }

    private Vector3 FindFreeLocation(List<Vector2Int> posList2DInt)
    {
        
        Vector2Int spawnPos2D = posList2DInt[(Random.Range(0, posList2DInt.Count))];
        Vector3 pos = Tool.Vector2IntToVector3(spawnPos2D);        
        posList2DInt.Remove(spawnPos2D); //Removing used position

        return pos;
    }

    private void SpawningPlayer(Dungeon dg)
    {
        //Getting player start pos
        Vector2Int playerStartPos2D = dg.kitchen.playerPos;
        Vector3 playerStartPos3D = new Vector3((float)playerStartPos2D.x, (float)playerStartPos2D.y, 0);

        //Instantiate player
        GameObject playerGO = Instantiate(playerPrefab, playerStartPos3D, Quaternion.identity);
        playerGO.name = "PLAYER";
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


