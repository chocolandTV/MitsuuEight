using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioVisualizer audioVisualizer;
    public static SoundManager Instance{get;private set;}
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioVisualizer = GetComponent<AudioVisualizer>();
    }
    public AudioVisualizer Audio_Visualizer()
    {
        return audioVisualizer;
    }
}
