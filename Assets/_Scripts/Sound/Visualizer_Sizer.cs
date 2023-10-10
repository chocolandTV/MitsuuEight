using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer_Sizer : MonoBehaviour
{
    [SerializeField] private float minSize,maxSize;
    private AudioVisualizer audioVisualizer;
    // Start is called before the first frame update
    void Start()
    {

        audioVisualizer = SoundManager.Instance.Audio_Visualizer();
        
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        gameObject.transform.localScale = new Vector3(
        audioVisualizer._audioBandBuffer[3]*maxSize + minSize,
        audioVisualizer._audioBandBuffer[3]*maxSize + minSize,
        audioVisualizer._audioBandBuffer[3]*maxSize + minSize);
        
        
        
    }
}
