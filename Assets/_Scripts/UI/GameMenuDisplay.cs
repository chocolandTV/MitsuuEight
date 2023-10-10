using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
public class GameMenuDisplay : MonoBehaviour
{
    // STAGE DISPLAY REFERENCES
    [SerializeField] private TextMeshProUGUI stageTextName, stageDescription, stageInfoValues;
    [SerializeField] private RawImage stageImage;
    [SerializeField] private GameObject stagelockedImage;
    // CAR DISPLAY REFERENCES
    [SerializeField]private TextMeshProUGUI carIdText, carDescriptionText;
    [SerializeField]private Image carMaxSpeed, carAccelerate, carHandling;
    [SerializeField] private RawImage carImage;
    [SerializeField] private GameObject carlockedImage;

    public void DisplayStage(SO_Stage _stage)
    {
        stageTextName.text = _stage.mapName;
        stageDescription.text = _stage.mapDescription;
        stageImage.texture = _stage.mapImage;
        // IF CONDITION !=  LOCK 
        stagelockedImage.SetActive(false);
    }
    public void DisplayCar(SO_Car _car)
    {
        carIdText.text = _car.CarName;
        carDescriptionText.text = _car.CarDescription;
        carMaxSpeed.fillAmount = _car.MaxSpeed;
        carAccelerate.fillAmount =  _car.Accelerate;
        carHandling.fillAmount =  _car.Handling;
        carImage.texture = _car.CarImage;
        // IF CONDITION !=  LOCK 
        carlockedImage.SetActive(false);
    }

}
