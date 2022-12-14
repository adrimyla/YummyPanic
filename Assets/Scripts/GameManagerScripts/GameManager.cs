using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    private GameObject playerGO;
    public GameObject playerSlotPrefab;
    public Transform playerItemContainer;

    [Header("Gluttons")]
    public List<GameObject> gluttonPrefabs;   
    [Range (2, 30)]    
    public int gluttonCount = 10;
    public GameObject burrowPrefab;
    [Range(2, 10)]
    public int burrowCount = 10;
    private GameObject gluttonsContainerGO;
    public GameObject burrowsContainerGO;

    [Header("Objects")]
    public int foodTotalCount = 10;
    public List<Item> food;
    public int bonusTotalCount = 10;
    public List<Item> bonus;
    private GameObject foodContainerGO;
    private GameObject bonusContainerGO; 

    [Header("Recipes")]
    public int minIngredientPerRecipe;
    public int maxIngredientPerRecipe;
    private RecipesManager RM;

    [Header("Countdown")]
    public GameObject countDownDisplayer;
    public float totalTimeInSeconds = 60f;
    private CountDownManager CM;

    [Header("Score")]
    public GameObject scoreDisplayer;
    public int actualScore;

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
        if(CM != null)
        {
            CM.UpdateCountDown();
        }
        
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
        Time.timeScale = 1;
        loadingScreen.SetActive(false);
        CM.StartCountDown();
    }

    private void HandlePauseMode()
    {
        CM.StopCountDown();
        Time.timeScale = 0;
    }

    private void HandleGameOver()
    {
        Time.timeScale = 0;
    }

    private void HandleLoading()
    {
        Debug.Log("Loading game ...");

        ResetGame();

        loadingScreen.SetActive(true);

        //====== STEP  1 : Generating dungeon and kitchen ======
        
        Dungeon dg = dungeon.GenerateDungeon();

        //====== STEP 2 : Placing player at spawn ======         

        SpawningPlayer(dg);

        //====== STEP 3 : Placing gluttons burrows (terriers) ======

        burrowsContainerGO = new GameObject();
        burrowsContainerGO.transform.parent = setup.transform;
        burrowsContainerGO.name = "BURROWS";
        SpawningGluttonsBurrows(dg);

        //====== STEP 4 : Placing gluttons on map ======

        gluttonsContainerGO = new GameObject();
        gluttonsContainerGO.name = "GLUTTONS";
        gluttonsContainerGO.transform.parent = setup.transform;   
        SpawningGluttons(dg);

        //====== STEP 5 : Placing food and bonus objects on map ======

        bonusContainerGO = new GameObject();
        bonusContainerGO.transform.parent = setup.transform;
        bonusContainerGO.name = "BONUS";
        SpawningBonusObjects(dg);

        foodContainerGO = new GameObject();
        foodContainerGO.transform.parent = bonusContainerGO.transform;
        foodContainerGO.name = "FOOD";
        SpawningFood(dg);

        //====== STEP 6 : Init player and gluttons stats (use default values of prefab) ======

        //====== STEP 7 : Init recipes manager, score and countdown ======

        // Init recipe manager
        RM = new RecipesManager(food, minIngredientPerRecipe, maxIngredientPerRecipe);
        Recipe r = RM.NewRandomRecipe();
        r.DebugPrintRecipe();

        //Init countdown manager
        CM = new CountDownManager(totalTimeInSeconds, countDownDisplayer);

        //Init score
        InitPlayerScore();

        //Start Game
        UpdateGameState(GameState.PLAYING); //When loading complete, we start the game !
    }

    public void UpdatePlayerScore(int value)
    {
        actualScore += value;
        var score = scoreDisplayer.GetComponent<TextMeshProUGUI>();
        score.text = actualScore.ToString();
    }

    public void InitPlayerScore()
    {
        actualScore = 0;
        var score = scoreDisplayer.GetComponent<TextMeshProUGUI>();
        score.text = actualScore.ToString();
    }

    private void ResetGame()
    {
        

        if (playerGO != null)
        {
            Destroy(playerGO);
        }

        if (gluttonsContainerGO != null)
        {
            Destroy(gluttonsContainerGO);
        }
        
        if (burrowsContainerGO != null)
        {
            Destroy(burrowsContainerGO);
        }

        if (bonusContainerGO != null)
        {
            Destroy(bonusContainerGO);
        }

        if(CM != null)
        {
            CM = null;
        }

        if(RM != null)
        {
            RM = null;
        }

        InitPlayerScore();        

    }

    private void SpawningGluttonsBurrows(Dungeon dg)
    {
        for (int i = 0; i < burrowCount; i++)
        {
            //Getting a random spawn location            
            Vector3 pos = FindFreeLocation(dg.freeFloorPositions);

            GameObject burrowGO = Instantiate(burrowPrefab, pos, Quaternion.identity);
            burrowGO.transform.parent = burrowsContainerGO.transform;

        }
    }
    private void SpawningBonusObjects(Dungeon dg)
    {
        for (int i = 0; i < bonusTotalCount; i++)
        {
            //Getting a random spawn location            
            Vector3 spawnPos3D = FindFreeLocation(dg.freeFloorPositions);

            //Getting a random element in bonus
            Item bonusItem = bonus[Random.Range(0, bonus.Count)];
            GameObject bonusGO = Instantiate(bonusItem.prefab, spawnPos3D, Quaternion.identity);
            bonusGO.transform.parent = bonusContainerGO.transform;

        }
    }

    private void SpawningFood(Dungeon dg)
    {
        for (int i = 0; i < foodTotalCount; i++)
        {
            //Getting a random spawn location            
            Vector3 spawnPos3D = FindFreeLocation(dg.freeFloorPositions);

            //Getting a random element in food
            Item foodItem = food[Random.Range(0, food.Count)];
            GameObject foodGO = Instantiate(foodItem.prefab, spawnPos3D, Quaternion.identity);
            foodGO.transform.parent = foodContainerGO.transform;

        }
    }

    private void SpawningGluttons(Dungeon dg)
    {   
        for(int i = 0; i < gluttonCount; i++)
        {
            //Getting a random spawn location
            Vector3 spawnPos3D = FindFreeLocation(dg.freeFloorPositions);

            //Choosing a random glutton prefab
            GameObject gluttonToSpawn = gluttonPrefabs[Random.Range(0, gluttonPrefabs.Count)];

            //Instantiate
            GameObject gluttonGO = Instantiate(gluttonToSpawn, spawnPos3D, Quaternion.identity);
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
        playerGO = Instantiate(playerPrefab, playerStartPos3D, Quaternion.identity);
        playerGO.name = "PLAYER";
        playerGO.transform.parent = setup.transform;

        //Adding player as object to follow by camera
        camFollow.ObjectToFollow = playerGO.transform;

        //Creating player inventory
        playerGO.transform.GetComponent<InventoryManager>().SlotItem = playerSlotPrefab;
        playerGO.transform.GetComponent<InventoryManager>().ItemContainer = playerItemContainer;
        playerGO.transform.GetComponent<InventoryManager>().ListItems(); //Clear inventory display
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


