using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class vacuum : MonoBehaviour
{
    [SerializeField] GameObject cleaner;
    [SerializeField] float speed = 10f;
    bool isCollide = false;
    private GameObject thing;
    private Transform theTransform;
    public Vector3 destination;
    private Vector3 dis;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destination = cleaner.transform.position;

        if (thing == null)
        {
            isCollide = false;
        }

        if (isCollide)
        {
            theTransform.LookAt(destination);
            theTransform.Translate(Vector3.forward * speed * Time.deltaTime);
            //theTransform.Translate(destination.normalized);
            //theTransform.Rotate(0, 0, 10);

            /*
            if (theTransform.localScale.x > 0 && theTransform.localScale.y > 0 && theTransform.localScale.z > 0)
            {
                //theTransform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                theTransform.localScale *= 0.9f;
            }
            */
            dis = theTransform.position - destination;

            if (absF(dis.x) < .5f && absF(dis.y) < .5f && absF(dis.z) < .5f)
            {
                if (theTransform.localScale.x > 0 && theTransform.localScale.y > 0 && theTransform.localScale.z > 0)
                {
                    //theTransform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
                    theTransform.localScale *= 0.95f;
                }
            }
            
        }

    }
    private Vector3 oriSize;

    private void OnTriggerEnter(Collider other)
    {
        thing = other.gameObject;
        Rigidbody rigi = other.GetComponent<Rigidbody>();
        //rigi.isKinematic = true;
        theTransform = thing.transform;
        oriSize = theTransform.localScale;
        theTransform.LookAt(destination);
        isCollide = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isCollide = false;
        theTransform.localScale = oriSize;
    }


    private float absF(float x)
    {
        return x < 0 ? -x : x;
    }
    /*
    private void OnTriggerStay(Collider other)
    {
        
    }
    */
}
