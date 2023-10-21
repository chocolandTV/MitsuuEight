using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer_CheckpointEffect : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem particleSystem_01, particleSystem_02;
    private AudioVisualizer audioVisualizer;
    [SerializeField]private float maxSize, minSize;
    
    // Start is called before the first frame update
    void Start()
    {

        audioVisualizer = SoundManager.Instance.Audio_Visualizer();
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        var main = particleSystem_01.main;
        main.startSpeedMultiplier = audioVisualizer._audioBandBuffer[1]*maxSize + minSize;

        var main2 = particleSystem_02.main;
        main2.startSpeedMultiplier = audioVisualizer._audioBandBuffer[1]*maxSize + minSize;

    }
}
