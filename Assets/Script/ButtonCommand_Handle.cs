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
        if (buttonName == "space") Debug.Log("Space");
        else if (buttonName == "r") Debug.Log("r");
        else if (buttonName == "z") Debug.Log("z");
        else if (buttonName == "escape") Debug.Log("escape");
    }
}
