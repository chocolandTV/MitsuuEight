using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun_Distance : MonoBehaviour
{
    [SerializeField]
    private float distance = 50;
    [SerializeField]
    private GameObject PlayerObject;
   
    void FixedUpdate()
    {
        
        transform.position = (transform.position - PlayerObject.transform.position).normalized * distance + PlayerObject.transform.position;
    }
}
