using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;

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
        mainMenu.SetActive(state == GameState.MAIN_MENU); // Display the main menu if game state is MAIN_MENU  
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
