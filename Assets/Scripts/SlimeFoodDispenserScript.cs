using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeFoodDispenserScript : MonoBehaviour
{
    [SerializeField] private Transform locationOfInstatiate;
    [SerializeField] private GameObject foodPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void InitFood()
    {
        Instantiate(foodPrefab, locationOfInstatiate.position+new Vector3(Random.Range(-0.05f,0.05f), Random.Range(-0.05f, 0.05f), Random.Range(-0.05f, 0.05f)), Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
