using System;
using UnityEngine;
public class ButtonCommand_Handle : MonoBehaviour
{
    private Input_handle inputHandle;
    private BubbleGum_UndoManager undoManager;

    private void Start()
    {
        undoManager = GetComponent<BubbleGum_UndoManager>();
        inputHandle = GetComponent<Input_handle>();

        if (inputHandle != null)
        {
            inputHandle.AddButtonListener(OnKeyBoardButtonPress);
            inputHandle.AddRegisterStateListener(RegisterAllCharacterStates);
        }
        else
        {
            Debug.LogError($"[{gameObject.name}] Input_handle is missing!");
        }

        if (undoManager == null)
        {
            Debug.LogWarning($"[{gameObject.name}] UndoManager is not assigned.");
        }
    }


    private void OnKeyBoardButtonPress(string buttonName)
    {
        if (buttonName == "space") Space_button_Action?.Invoke();

        else if (buttonName == "z")
        {
            foreach (var character in BubbleGum_UndoManager.AllCharacters)
            {
                character.UndoState();
            }
        }

        else if (buttonName == "r")
        {
            foreach (var character in BubbleGum_UndoManager.AllCharacters)
            {
                character.RedoState();
            }
        }

        else if (buttonName == "escape") Debug.Log("escape");
    }



    Action Space_button_Action;


    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }
    private void RegisterAllCharacterStates()
    {
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.RegisterState();
        }
    }




}
