using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrakeLight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Light[] breakLightsInner;
    [SerializeField] Light[] breakLightsOuter;
    [SerializeField] GameObject backLight01, backLight02;
    public static CarBrakeLight Instance{get; private set;}

   
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }
   
    public void CarBrakeVisualEffect(float intensity)
    {
        
        breakLightsOuter[0].gameObject.SetActive(true);
        breakLightsOuter[1].gameObject.SetActive(true);

        breakLightsInner[0].intensity = intensity;
        breakLightsInner[1].intensity = intensity;
    }
    public void CarBrakeVisualEffectOff()
    {
        
        breakLightsOuter[0].gameObject.SetActive(false);
        breakLightsOuter[1].gameObject.SetActive(false);

        breakLightsInner[0].intensity = 0.2f;
        breakLightsInner[1].intensity =0.2f;
    }
    public void SetCarBackLights(bool value)
    {
        backLight01.SetActive(value);
        backLight02.SetActive(value);
    }

}
