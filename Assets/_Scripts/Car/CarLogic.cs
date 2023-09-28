using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarLogic : MonoBehaviour
{
    [SerializeField]
    private CarController m_car;

    // Start is called before the first frame update
     private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectable"))
        {
            m_car.AddBoostCapacity(other.GetComponent<Nitro>().NitroValue);
            other.GetComponent<Nitro>().Collect();
        }
         if(other.CompareTag("Respawn"))
        {
            m_car.ResetPosition();
        }
    }
}
