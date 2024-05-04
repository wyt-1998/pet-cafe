using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class dropDown : MonoBehaviour
{
    private GameObject locomotionType;
    [SerializeField] private GameObject rayInteractionL;
    [SerializeField] private GameObject rayInteractionR;



    public void MovementSelect(int n)
    {
        locomotionType = GameObject.Find("Locomotion System");


        if (n ==0)
        {
            locomotionType.GetComponent<ActionBasedContinuousMoveProvider>().enabled = false;
            locomotionType.GetComponent<TeleportationProvider>().enabled = true;
            rayInteractionR.SetActive(true);
            rayInteractionL.SetActive(true);
        }
        else
        {
            locomotionType.GetComponent<ActionBasedContinuousMoveProvider>().enabled = true;
            locomotionType.GetComponent<TeleportationProvider>().enabled = false;
            rayInteractionR.SetActive(false);
            rayInteractionL.SetActive(false);
        }
    }

}
