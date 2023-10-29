using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get;private set;}

    public enum GameState
    {
       MainMenu,
       StageStart,
       StageRunning,
       StageEnd,
       Selection,
       Pause
    }
    GameState currentState;
    public static int Game_CurrentStage {get; private set;}=1;
    public static int Game_CurrentCar {get; private set;}=1;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
    public void SetState(GameState state)
    {
        currentState = state;
        switch (state)
        {
            case GameState.MainMenu:
                Debug.Log("Main Menu");
                break;
            case GameState.StageStart:
                Debug.Log("Stage Start");
                break;
            case GameState.StageRunning:
                Debug.Log("Stage running");
                break;
            case GameState.StageEnd:
                Debug.Log("Stage end");
                break;
            case GameState.Selection:
                Debug.Log("Selection");
                break;
            case GameState.Pause:
                Debug.Log("Game Paused");
                break;
            default:
                Debug.Log("Do nothing");
                break;
        }
    }
}   
