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
            m_car.AddCarNitro(other.GetComponent<Nitro>().NitroValue);
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
        if(other.CompareTag("Collectable_Cash"))
        {
            Debug.Log("Collect 1coin ");
            CollectionManager.CoinWallet ++;
            Destroy(other.gameObject);
        }
        if(other.CompareTag("Obstacle"))
        {
            Debug.Log("ObstacleCrash - Get Damage");
            m_car.AddLife(-other.GetComponent<ObstacleDamage>().Damage);
            Destroy(other.gameObject);
        }
    }
}
