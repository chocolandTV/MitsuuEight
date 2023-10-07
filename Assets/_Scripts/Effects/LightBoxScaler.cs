using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBoxScaler : MonoBehaviour
{
    [SerializeField] private int BandID;
    [SerializeField]private float startScale, maxScale;
    private AudioVisualizer _audioVisualizer;
    void Start()
    {
        _audioVisualizer = SoundManager.Instance.Audio_Visualizer();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(
            startScale,
            _audioVisualizer._audioBandBuffer[BandID]*maxScale + startScale,
            startScale
        );
    }
}
