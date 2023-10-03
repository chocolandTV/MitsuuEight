using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visualizer_EmissionEffect : MonoBehaviour
{
    private Material Emission_material, Emission_material1, Emission_material2, Emission_material3;

    private int audioBandID;
    private Color _orginalEmColor, _emColor;
    private AudioVisualizer audioVisualizer;
    // Start is called before the first frame update
    void Start()
    {
        Emission_material = gameObject.GetComponent<Renderer>().materials[0];
        Emission_material1 = gameObject.GetComponent<Renderer>().materials[1];
        Emission_material2 = gameObject.GetComponent<Renderer>().materials[2];
        Emission_material3 = gameObject.GetComponent<Renderer>().materials[4];
        _orginalEmColor = gameObject.GetComponent<Renderer>().material.color;
        audioVisualizer = SoundManager.Instance.Audio_Visualizer();
        audioBandID = Random.Range(0,7);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _emColor = _orginalEmColor * audioVisualizer._audioBandBuffer64[audioBandID - 1];
        Emission_material.SetColor("_EmissionColor", _emColor);
        Emission_material1.SetColor("_EmissionColor", _emColor);
        Emission_material2.SetColor("_EmissionColor", _emColor);
        Emission_material3.SetColor("_EmissionColor", _emColor);
    }
}
