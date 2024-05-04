using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class teleportationManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private GameObject leftInteractor;
    private XRRayInteractor rayInteractorL;
    private XRInteractorLineVisual lineRenderableL;
    //[SerializeField] private GameObject RightInteractor;
    //private XRRayInteractor rayInteractorR;
    //private XRInteractorLineVisual lineRenderableR;
    [SerializeField] private TeleportationProvider provider;
    [SerializeField] private bool isTeleportActive = false;
    private InputAction thumbstickL;
    //private InputAction thumbstickR;
    private GameObject anchor;
    
    // Start is called before the first frame update
    void Start()
    {
        lineRenderableL = leftInteractor.GetComponent<XRInteractorLineVisual>();
        lineRenderableL.enabled = false;
        rayInteractorL = leftInteractor.GetComponent<XRRayInteractor>();
        rayInteractorL.enabled = false;

        //lineRenderableR = RightInteractor.GetComponent<XRInteractorLineVisual>();
        //lineRenderableR.enabled = false;
        //rayInteractorR = RightInteractor.GetComponent<XRRayInteractor>();
        //rayInteractorR.enabled = false;

        var activateL = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activateL.Enable();
        activateL.performed += OnTeleportActivate;

        var cancelL = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancelL.Enable();
        cancelL.performed += OnTeleportCancel;

        thumbstickL = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        thumbstickL.Enable();

        //var activateR = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Activate");
        //activateR.Enable();
        //activateR.performed += OnTeleportActivate;

        //var cancelR = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Teleport Mode Cancel");
        //cancelR.Enable();
        //cancelR.performed += OnTeleportCancel;

        //thumbstickR = actionAsset.FindActionMap("XRI RightHand Locomotion").FindAction("Move");
        //thumbstickR.Enable();

    }

    // Update is called once per frame
    void Update()
    {

        if (isTeleportActive & !thumbstickL.triggered)
        {
            if (!rayInteractorL.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                DisableLineRender();
                return;
            }
            if (hit.transform.gameObject.GetComponent<TeleportationArea>() == null)
            {
                Debug.Log("cant move there");
                DisableLineRender();
                return;
            }

            Debug.Log("teleport by Left");
            TeleportRequest request = new TeleportRequest()
            {
                destinationPosition = hit.point
                //destinationRotation = transform.rotation
            };
            provider.QueueTeleportRequest(request);
            DisableLineRender();
        }
    }

    public void OnTeleportActivate(InputAction.CallbackContext context)
    {
        Debug.Log("tele actiated");
        if (lineRenderableL != null)
        {
            lineRenderableL.enabled = true;
            rayInteractorL.enabled = true;
            isTeleportActive = true;
        }
    }

    public void OnTeleportCancel(InputAction.CallbackContext context)
    {
        Debug.Log("tele cancel");
        DisableLineRender();
    }

    private void DisableLineRender()
    {
        rayInteractorL.enabled = false;
        lineRenderableL.enabled = false;
        //rayInteractorR.enabled = false;
        //lineRenderableR.enabled = false;
        isTeleportActive = false;
        anchor = GameObject.FindGameObjectWithTag("anchor");
        if (anchor != null) anchor.SetActive(false);
    }
}
