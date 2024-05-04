//reference https://www.youtube.com/watch?v=9dc1zq8eH54 with adjustment
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class RayInteractToggle : MonoBehaviour
{
    [SerializeField] private InputActionReference activateReference = null;

    private XRRayInteractor rayInteractor = null;
    private bool isEnabled = false;
    private bool isPressed = false;
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
        if (isEnabled && isPressed == false)//on pressed
        {
            isPressed = true;
            Invoke("DisableRay", 0.2f);
        }
        if (!isEnabled && isPressed == true)
        {
            isPressed = false;
            rayInteractor.enabled = true;
        }
    }
    public void DisableRay()
    {
        if (isEnabled == true)
        {
            rayInteractor.enabled = false;
        }
    }

}
