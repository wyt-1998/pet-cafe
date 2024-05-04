/* reference link https://www.youtube.com/watch?v=VG8hLKyTiJQ */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPresencePhysics : MonoBehaviour
{
    [SerializeField] private Transform Target;
    [SerializeField] private Renderer nonPhysicalHand;
    [SerializeField] private float distanceUntilShowGhostHand = 0.05f;
    [SerializeField] private float distance;
    private Collider[] handcolliders;
    private bool grabing;

    private Rigidbody rb;
    // Start is called before the first frame update
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        handcolliders = GetComponentsInChildren<Collider>();
    }

    public void EnableHandCollider()
    {
        Debug.Log("enable col");
        foreach (var item in handcolliders)
        {
            item.enabled = true;
        }
    }
    public void EnableHandColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }

    public void DisableHandCollider()
    {
        Debug.Log("Disable col");
        foreach (var item in handcolliders)
        {
            item.enabled = false;
        }
    }
    // Update is called once per frame
    private void Update()
    {
        distance = Vector3.Distance(Target.position, transform.position);

        if (distance > distanceUntilShowGhostHand)
        {
            nonPhysicalHand.enabled = true;
        }
        else
        {
            nonPhysicalHand.enabled = false;
        }
    }
    void FixedUpdate() 
    {
        rb.velocity = (Target.position - transform.position) / Time.fixedDeltaTime;

        Quaternion rotationDifference = Target.rotation * Quaternion.Inverse(transform.rotation);
        rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        rb.angularVelocity = (rotationDifferenceInDegree * Mathf.Deg2Rad / Time.fixedDeltaTime);
    }
}
