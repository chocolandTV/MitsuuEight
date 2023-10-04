using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int id;
    public void Collect_Item()
    {
        CollectionManager.Stage01_Collection.Add(id);
        Destroy(gameObject);
    }
}
