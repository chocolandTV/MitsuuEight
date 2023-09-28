using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    public int NitroValue{get;private set;}

    public void SetNitro(int amount)
    {
        NitroValue = amount;
    }
    public void Collect()
    {
        NitroValue = 0;
        Destroy(gameObject);
    }
}
