using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGenScript : MonoBehaviour
{
    [SerializeField] private Transform initSlimePosition;
    [Header("slime type")]
    [SerializeField] private GameObject[] Slimes;
    [SerializeField] private GameObject SSRslimes;
    private bool isActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        { 
            if (transform.position.y > 1.35f)
            {

                isActivated = false;
            }
        }
        else
        {
            if (transform.position.y < 1.08)
            {
                Instantiate(GetRandomSlime(), initSlimePosition.position, Random.rotation);
                isActivated = true;
            }
        }
    }
    private GameObject GetRandomSlime()
    {
        var randIndex = Random.Range(0, Slimes.Length);
        if (Random.Range(0, 10) == 0){
            return SSRslimes;
        }
        return Slimes[Random.Range(0, Slimes.Length)];
    }
}
