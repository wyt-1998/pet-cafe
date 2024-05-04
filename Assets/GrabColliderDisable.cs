using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabColliderDisable : MonoBehaviour
{
    [SerializeField] private GameObject hand;
    private Collider[] colliders;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        colliders = hand.transform.GetComponentsInChildren<Collider>();
    }
    public void isGrab()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = false;
        }
    }
    public void isRelease()
    {
        StartCoroutine(EnableCol());
    }
    IEnumerator EnableCol()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slime Food B"))
        {
            Destroy(collision.gameObject);
            if (rb.useGravity == false)
            {
                rb.useGravity = true;
            }
            else
            {
                rb.useGravity = false;
            }
            return;
        }
    }
}
