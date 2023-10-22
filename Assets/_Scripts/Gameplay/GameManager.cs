using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}

    private enum GameState
    {
       MainMenu,
       Shop,
       Stage,
       GameOver,
       GameDone,
       Paused
    }
    private GameState current_GameState;
    public static int Game_stageIndex {get; private set;}=1;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
}   
