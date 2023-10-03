using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]private AudioVisualizer audioVisualizer;
    public static SoundManager Instance{get;private set;}

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public AudioVisualizer Audio_Visualizer()
    {
        return audioVisualizer;
    }
}
