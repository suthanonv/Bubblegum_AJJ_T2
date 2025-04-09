using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonCommand_Handle : MonoBehaviour
{
    
    private Input_handle inputHandle;
    [SerializeField] private UndoAndRedoController undoController;
    [SerializeField] private LevelLoader levelLoader;

    private void Start()
    {
        inputHandle = GetComponent<Input_handle>();

        if (inputHandle != null)
        {
            inputHandle.AddButtonListener(OnKeyBoardButtonPress);
        }
        if (undoController == null || inputHandle ==null || undoController == null)
        {
            Debug.LogError($"[{gameObject.name}] undoController is missing!");
        }
        if (inputHandle == null)
        {
            Debug.LogError($"[{gameObject.name}] input_handle is missing!");
        }
        if (undoController == null)
        {
            Debug.LogError($"[{gameObject.name}] undoController is missing!");
        }

    }



    private void OnKeyBoardButtonPress(string buttonName)
    {
        if (buttonName == "space") Space_button_Action?.Invoke();

        else if (buttonName == "q") undoController.Undo();

        else if (buttonName == "e") undoController.Redo();

        else if (buttonName == "r") levelLoader.reloadScene();

        else if (buttonName == "o") levelLoader.loadPreviousScene();

        else if (buttonName == "p") levelLoader.loadNextScene();

        else if (buttonName == "escape") Debug.Log("escape");
    }



    Action Space_button_Action;


    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }
    




}
