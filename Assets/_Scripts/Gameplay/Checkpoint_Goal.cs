using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LapManager.Instance.CheckNextRound(other.gameObject.transform.position);
            // sound !
        }
    }
}
