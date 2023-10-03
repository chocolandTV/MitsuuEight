using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildingBrickMaterial : MonoBehaviour
{
    Renderer mesh;  
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("do Start");
        mesh = gameObject.GetComponent<Renderer>();
        mesh.SetMaterials(CityManager.Instance.GetBuildingMaterial()); 
        
    }
    

}
