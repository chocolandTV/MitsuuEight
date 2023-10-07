using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(ParticleSystem))]
public class particleEffect : MonoBehaviour
{
    private AudioVisualizer audioVisualizer;
    public float minSpeed;
    public int _audioBandID;
    
    void Start()
    {
        audioVisualizer = SoundManager.Instance.Audio_Visualizer();
    }
    void Update()
    {
        
        var velocityOverLifetime = GetComponent<ParticleSystem>().velocityOverLifetime;
        //  velocityOverLifetime.zMultiplier = minSpeed * audioPeer._audioBand[_audioBandID];
        velocityOverLifetime.zMultiplier = Mathf.Clamp(3 * audioVisualizer._audioBand[_audioBandID],minSpeed, 3);
        var size = GetComponent<ParticleSystem>().sizeOverLifetime;
        size.size = 1 * audioVisualizer._audioBand[_audioBandID];
           
    }
}