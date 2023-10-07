using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int Checkpoint_ID;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LapManager.Instance.CheckpointSet(Checkpoint_ID, other.gameObject.transform.position);
            // sound !
        }
    }
}
