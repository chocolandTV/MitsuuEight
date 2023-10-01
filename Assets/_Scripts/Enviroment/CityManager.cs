using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityManager : MonoBehaviour
{
    [SerializeField] private List<Material> materials;
    [SerializeField] private Material mat_default;
    public static CityManager Instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

    }

    // Update is called once per frame
    public List<Material> GetBuildingMaterial()
    {
        List<Material> maty = new List<Material>();
        maty.Add(materials[Random.Range(0, materials.Count)]);
        maty.Add(materials[Random.Range(0, materials.Count)]);
        maty.Add(materials[Random.Range(0, materials.Count)]);
        maty.Add(mat_default);
        maty.Add(materials[Random.Range(0, materials.Count)]);

        return maty;
    }

}
