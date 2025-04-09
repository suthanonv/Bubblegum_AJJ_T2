using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ButtonCommand_Handle : MonoBehaviour
{
    
    private Input_handle inputHandle;
    [SerializeField] private UndoAndRedoController undoController;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private float inputCooldown = 0.25f;
    private float nextMoveTime = 0f;

    private string _buttonCommand;
    public string buttonCommand => _buttonCommand;

    private void Start()
    {
        inputHandle = GetComponent<Input_handle>();

        if (inputHandle != null)
        {
            inputHandle.AddButtonListener(OnKeyBoardButtonPress);
        }
        if (inputHandle == null) Debug.LogError("[ButtonCommand_Handle] Input_handle missing!");
        if (undoController == null) Debug.LogError("[ButtonCommand_Handle] UndoAndRedoController missing!");


    }



    private void OnKeyBoardButtonPress(string buttonName)
    {
        if (Time.time >= nextMoveTime)
        {
            buttonName = buttonName.ToLowerInvariant();
            _buttonCommand = buttonName;

            if (buttonName == "space") Space_button_Action?.Invoke();
            else if (buttonName == "z") undoController.Undo();
            else if (buttonName == "r") levelLoader.reloadScene();
            else if (buttonName == "o") levelLoader.loadPreviousScene();
            else if (buttonName == "p") levelLoader.loadNextScene();
            else if (buttonName == "escape") Debug.Log("escape pressed");

            nextMoveTime = Time.time + inputCooldown;
        }
    }




    Action Space_button_Action;


    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }
    




}
