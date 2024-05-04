using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodLauncher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] foods;
    [SerializeField] private GameObject initPos;
    private List<GameObject> storedFood = new List<GameObject>();
    [SerializeField] private GameObject foodA;
    [SerializeField] private float thrust = 20f;
    [SerializeField] private GameObject foodB;
    [SerializeField] private GameObject slime;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Slime Food B") || other.CompareTag("Slime Food A") || other.CompareTag("Slime"))
        {
            storedFood.Add(other.gameObject);
            other.gameObject.SetActive(false);
            displayFood();
        }
    }
    public void shoot()
    {
        if (storedFood.Count > 0)
        {
            storedFood[0].SetActive(true);
            storedFood[0].transform.position = initPos.transform.position;
            storedFood[0].GetComponent<Rigidbody>().AddForce(initPos.transform.forward* thrust);
            storedFood.RemoveAt(0);
            displayFood();
        }

    }
    public void displayFood()
    {
        if (storedFood.Count > 0)
        {
            if (storedFood[0].CompareTag("Slime Food B"))
            {
                foodB.SetActive(true);
                foodA.SetActive(false);
                slime.SetActive(false);


            }
            if (storedFood[0].CompareTag("Slime Food A"))
            {
                foodB.SetActive(false);
                foodA.SetActive(true);
                slime.SetActive(false);

            }
            if (storedFood[0].CompareTag("Slime"))
            {
                foodB.SetActive(false);
                foodA.SetActive(false);
                slime.SetActive(true);
            }
        }
        else
        {
            foodB.SetActive(false);
            foodA.SetActive(false);
            slime.SetActive(false);
        }
    }
}
