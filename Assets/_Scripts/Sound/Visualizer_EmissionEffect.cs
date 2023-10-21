using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer_EmissionEffect : MonoBehaviour
{
    [SerializeField]private Material material, material2;
    private int audioBand;
    [SerializeField]private Color color, color2;
    private AudioVisualizer audioVisualizer;
    // Start is called before the first frame update
    void Start()
    {

        audioVisualizer = GetComponent<AudioVisualizer>();
        // audioBand=Random.Range(2,7);
        // color= material.GetVector("_EmissionColor");
        // color2= material2.GetVector("_EmissionColor");
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        material.SetVector("_EmissionColor", color* audioVisualizer._audioBandBuffer[0]*5);
        material2.SetVector("_EmissionColor", color2* audioVisualizer._audioBandBuffer[4]*5);
        
        
    }
}
