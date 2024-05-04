//reference https://www.youtube.com/watch?v=lPPa9y_czlE with adjustment to fit into our project
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject button;
    [SerializeField] private Material green;
    private Material originalMaterial; 
    [SerializeField] private GameObject buttonBase;
    [SerializeField] private Transform posBottonPress;
    [SerializeField] private UnityEvent onPress;
    [SerializeField] private UnityEvent onRelease;
    private Vector3 buttonOriginalPosition;
    GameObject presser;
    AudioSource sound;
    bool isPressed;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterial = button.GetComponent<MeshRenderer>().material;
        buttonOriginalPosition = button.transform.localPosition;
        sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed && other.gameObject != buttonBase && other.gameObject.name != "RightHand Controller" && other.gameObject.name != "LeftHand Controller")
        {
            
            //Debug.Log("pressed" + other.gameObject.name);
            button.GetComponent<MeshRenderer>().material = green;
            button.transform.localPosition = posBottonPress.localPosition;
            presser = other.gameObject;
            onPress.Invoke();
            sound.Play();
            isPressed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("exit");
        if (other.gameObject == presser) // check if the object left the trigger area is the object that pressed it
        {
            //Debug.Log("stop pressed");
            button.GetComponent<MeshRenderer>().material = originalMaterial;
            button.transform.localPosition = buttonOriginalPosition;
            onRelease.Invoke();
            isPressed = false;
        }
    }
}
