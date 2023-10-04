using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionManager : MonoBehaviour
{
    public static List<int> Stage01_Collection;
    public static List<int> Stage02_Collection;
    public static List<int> Stage03_Collection;
    public static List<int> Stage04_Collection;
    public static List<int> Stage05_Collection;
    public static List<int> Stage06_Collection;
    public static List<int> Stage07_Collection;
    public static List<int> Stage08_Collection;

    public static CollectionManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public List<int> getCollection(int id)
    {
        switch (id)
        {
            case 1:
                return Stage01_Collection;

            case 2:
                return Stage02_Collection;

            case 3:
                return Stage03_Collection;

            case 4:
                return Stage04_Collection;

            case 5:
                return Stage05_Collection;

            case 6:
                return Stage06_Collection;

            case 7:
                return Stage07_Collection;

            case 8:
                return Stage08_Collection;
            default:
                return Stage01_Collection;
        }
    }

}
