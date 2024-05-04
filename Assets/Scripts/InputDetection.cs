using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputDetection : MonoBehaviour
{
    private bool Input_RightTrigger = false;
    private bool Input_LeftTrigger = false;
    private bool Input_RightGrip = false;
    private bool Input_LeftGrip = false;

    private bool Input_RT_OnPress = false;
    private bool Input_RT_OnRelease = false;
    private bool Input_LT_OnPress = false;
    private bool Input_LT_OnRelease = false;
    private bool Input_RG_OnPress = false;
    private bool Input_RG_OnRelease = false;
    private bool Input_LG_OnPress = false;
    private bool Input_LG_OnRelease = false;

    private bool RT_cur_data;
    private bool LT_cur_data;
    private bool RG_cur_data;
    private bool LG_cur_data;

    private bool RT_prev_data;
    private bool LT_prev_data;
    private bool RG_prev_data;
    private bool LG_prev_data;


    public bool Get_RT_OnPress()
    {
        return Input_RT_OnPress;
    }
    public bool Get_RT_OnRelease()
    {
        return Input_RT_OnRelease;
    }

    public bool Get_LT_OnPress()
    {
        return Input_LT_OnPress;
    }
    public bool Get_LT_OnRelease()
    {
        return Input_LT_OnRelease;
    }

    public bool Get_RG_OnPress()
    {
        return Input_RG_OnPress;
    }
    public bool Get_RG_OnRelease()
    {
        return Input_RG_OnRelease;
    }

    public bool Get_LG_OnPress()
    {
        return Input_LG_OnPress;
    }
    public bool Get_LG_OnRelease()
    {
        return Input_LG_OnRelease;
    }

    public bool Get_Input_RT()
    {
        return Input_RightTrigger;
    }
    public bool Get_Input_LT()
    {
        return Input_LeftTrigger;
    }
    public bool Get_Input_RG()
    {
        return Input_RightGrip;
    }
    public bool Get_Input_LG()
    {
        return Input_LeftGrip;
    }

    //Right Trigger Detection 
    public void OnInputAction_RightTrigger(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            if (callbackContext.started)
            {
                this.Input_RightTrigger = true;
            }
            if (callbackContext.canceled)
            {
                this.Input_RightTrigger = false;
            }
            return;
        }
    }
    //Left Trigger Detection
    public void OnInputAction_LeftTrigger(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            if (callbackContext.started)
            {
                this.Input_LeftTrigger = true;
            }
            if (callbackContext.canceled)
            {
                this.Input_LeftTrigger = false;
            }
            return;
        }
    }

    //Right Grip Detection
    public void OnInputAction_RightGrip(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            if (callbackContext.started)
            {
                this.Input_RightGrip = true;
            }
            if (callbackContext.canceled)
            {
                this.Input_RightGrip = false;
            }
            return;
        }
    }
    //Left Grip Detection
    public void OnInputAction_LeftGrip(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.action.type == InputActionType.Button)
        {
            if (callbackContext.started)
            {
                this.Input_LeftGrip = true;
            }
            if (callbackContext.canceled)
            {
                this.Input_LeftGrip = false;
            }
            return;
        }
    }
    void Update()
    {
        RT_cur_data = Input_RightTrigger;
        LT_cur_data = Input_LeftTrigger;
        RG_cur_data = Input_RightGrip;
        LG_cur_data = Input_LeftGrip;
        //reset
        Input_RT_OnPress = false;
        Input_RT_OnRelease = false;
        Input_LT_OnPress = false;
        Input_LT_OnRelease = false;
        Input_RG_OnPress = false;
        Input_RG_OnRelease = false;
        Input_LG_OnPress = false;
        Input_LG_OnRelease = false;
        //right Trigger
        if (!RT_prev_data && RT_cur_data)
        {
            Input_RT_OnPress = true;
        }
        else if (RT_prev_data && !RT_cur_data)
        {
            Input_RT_OnRelease = true;
        }
        //left Trigger
        if (!LT_prev_data && LT_cur_data)
        {
            Input_LT_OnPress = true;
        }
        else if (LT_prev_data && !LT_cur_data)
        {
            Input_LT_OnRelease = true;
        }
        //right Gripper
        if (!RG_prev_data && RG_cur_data)
        {
            Input_RG_OnPress = true;
        }
        else if (RG_prev_data && !RG_cur_data)
        {
            Input_RG_OnRelease = true;
        }
        //left Gripper
        if (!LG_prev_data && LG_cur_data)
        {
            Input_LG_OnPress = true;
        }
        else if (LG_prev_data && !LG_cur_data)
        {
            Input_LG_OnRelease = true;
        }

        RT_prev_data = RT_cur_data;
        LT_prev_data = LT_cur_data;
        RG_prev_data = RG_cur_data;
        LG_prev_data = LG_cur_data;
    }
}
