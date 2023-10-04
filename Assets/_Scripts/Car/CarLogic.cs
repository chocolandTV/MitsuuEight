using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CarLogic : MonoBehaviour
{
    [SerializeField]
    private CarController m_car;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            Debug.Log("Collect Nitro");
            m_car.AddLife(other.GetComponent<Nitro>().NitroValue);
            other.GetComponent<Nitro>().Collect();
        }
        if (other.CompareTag("Respawn"))
        {
            Debug.Log("Respawn");
            m_car.ResetPosition();
        }
        if (other.CompareTag("BoostField"))
        {
            Debug.Log("BoostField activated");
            m_car.StartBoostField();
        }
        if (other.CompareTag("Collectable_S"))
        {
            Debug.Log("collect Eight");
            other.GetComponent<Collectable>().Collect_Item();
        }
    }
}
