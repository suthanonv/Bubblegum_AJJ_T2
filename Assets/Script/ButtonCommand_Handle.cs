using System;
using UnityEngine;
public class ButtonCommand_Handle : MonoBehaviour
{
    private Input_handle inputHandle;

    private void Start()
    {
        inputHandle = GetComponent<Input_handle>();
        if (inputHandle != null)
        {
            inputHandle.AddButtonListener(OnButtonPress);
        }
        else
        {
            Debug.LogError("Input_handle is not assigned.");
        }
    }

    private void OnButtonPress(string buttonName)
    {
        if (buttonName == "space") Space_button_Action.Invoke();
        else if (buttonName == "r") Debug.Log("r");
        else if (buttonName == "z") Debug.Log("z");
        else if (buttonName == "escape") Debug.Log("escape");
    }


    Action Space_button_Action;

    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }
}
