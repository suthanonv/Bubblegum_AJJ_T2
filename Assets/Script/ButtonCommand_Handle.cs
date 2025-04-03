using System;
using UnityEngine;
public class ButtonCommand_Handle : MonoBehaviour
{
    private Input_handle inputHandle;
    private UndoAndRedoController undoController;

    private void Start()
    {
        inputHandle = GetComponent<Input_handle>();
        undoController = GetComponent<UndoAndRedoController>(); 

        if (inputHandle != null)
        {
            inputHandle.AddButtonListener(OnKeyBoardButtonPress);
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] Input_handle is missing!");
        }

        if (undoController == null)
        {
            Debug.LogError($"[{gameObject.name}] UndoAndRedoController not found in scene!");
        }
    }



    private void OnKeyBoardButtonPress(string buttonName)
    {
        if (buttonName == "space") Space_button_Action?.Invoke();

        else if (buttonName == "z") undoController.Undo();


        else if (buttonName == "r") undoController.Redo();

        else if (buttonName == "escape") Debug.Log("escape");
    }



    Action Space_button_Action;


    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }
    




}
