using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class UndoAndRedoController : MonoBehaviour
{
    private Input_handle inputHandle;
    private Get_Input get_input;
    private BubbleGum_UndoManager bubblegum_undoManager;
    private Box_UndoAndRedo box_UndoAndRedo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        get_input = GetComponent<Get_Input>();
        inputHandle = GetComponent<Input_handle>();
        bubblegum_undoManager = GetComponent<BubbleGum_UndoManager>();
        box_UndoAndRedo = GetComponent<Box_UndoAndRedo>();
        if (inputHandle != null)
        {
            inputHandle.AddRegisterStateListener(RegisterAllCharacterStates);
        }
    }
    public void Undo()
    {
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.Undo();
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.Undo();
        }
    }
    public void Redo()
    {
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.Redo();
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.Redo();
        }
    }
    
    private void RegisterAllCharacterStates()
    {
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.RegisterState(get_input.direction);
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.RegisterState();
        }
    }

}
