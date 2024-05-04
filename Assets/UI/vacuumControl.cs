using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vacuumControl : MonoBehaviour
{
    private GameObject spark;

    // Start is called before the first frame update
    void Start()
    {
        spark = GameObject.Find("effect");
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3 || collision.gameObject.CompareTag("Slime Food A") || collision.gameObject.CompareTag("Slime Food B"))
        {
            Destroy(collision.gameObject);
            spark.GetComponent<Animator>().SetTrigger("playAni");

        }
    }
}
