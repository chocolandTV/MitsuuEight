using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] stageObjects;
    [SerializeField]private ScriptableObject[] carObjects;
    [SerializeField]private GameMenuDisplay gameMenuDisplay;
    private int currentStage = 0;
    private int currentCar =0;

    private void Awake()
    {
        gameMenuDisplay.DisplayStage((SO_Stage)stageObjects[0]);
        gameMenuDisplay.DisplayCar((SO_Car)carObjects[0]);
    }
    public void ChangeStage(int _change)
    {
        // INT INDEX
        currentStage += _change;
        if(currentStage < 0) currentStage = stageObjects.Length -1;
        else if(currentStage > stageObjects.Length -1) currentStage =0;

        if(gameMenuDisplay != null) gameMenuDisplay.DisplayStage((SO_Stage)stageObjects[currentStage]);
    }
    public void ChangeCar(int _change)
    {
        // INT INDEX
        currentCar += _change;
        if(currentCar < 0) currentCar = carObjects.Length -1;
        else if(currentCar > carObjects.Length -1) currentCar =0;

        if(gameMenuDisplay != null) gameMenuDisplay.DisplayCar((SO_Car)carObjects[currentCar]);
    }
    public void StartStage()
    {
        // SET CAR 
        // LOAD STAGE
        SceneManager.LoadScene(currentStage);
    }
}
