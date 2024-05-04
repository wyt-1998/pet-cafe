//reference https://www.youtube.com/watch?v=9dc1zq8eH54 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggler : MonoBehaviour
{
    [SerializeField] private InputActionReference activateReference = null;

    private XRRayInteractor rayInteractor = null;
    private bool isEnabled = false;
    // Start is called before the first frame update
    private void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
    }
    private void OnEnable()
    {
        activateReference.action.started += ToggleRay;
        activateReference.action.canceled += ToggleRay;
    }

    private void OnDisable()
    {
        activateReference.action.started -= ToggleRay;
        activateReference.action.canceled -= ToggleRay;
    }
    private void ToggleRay(InputAction.CallbackContext context)
    {
        isEnabled = context.control.IsPressed();
    }

    private void LateUpdate()
    {
        ApplyStatus();
    }
    private void ApplyStatus()
    {
        if(rayInteractor.enabled != isEnabled)
        {
            rayInteractor.enabled = isEnabled;
        }
    }
}
