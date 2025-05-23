using System;
using UnityEngine;


public class ButtonCommand_Handle : Singleton<ButtonCommand_Handle>
{

    [SerializeField] private float inputCooldown = 0.25f;
    private float nextMoveTime = 0f;

    private string _buttonCommand;
    public string buttonCommand => _buttonCommand;

    private void Start()
    {

        Input_handle.Instance.AddButtonListener(OnKeyBoardButtonPress);


    }



    private void OnKeyBoardButtonPress(string buttonName)
    {
        if (Time.time >= nextMoveTime)
        {
            buttonName = buttonName.ToLowerInvariant();
            _buttonCommand = buttonName;

            if (buttonName == "space") Space_button_Action?.Invoke();
            else if (buttonName == "z") UndoAndRedoController.Instance.Undo();
            else if (buttonName == "r") LevelLoader.Instance.reloadScene();
            else if (buttonName == "o") LevelLoader.Instance.loadPreviousScene();
            else if (buttonName == "p") LevelLoader.Instance.loadNextScene();
            else if (buttonName == "escape") Esc_button_Action?.Invoke();

            nextMoveTime = Time.time + inputCooldown;
        }
    }


    public void Add_Esc_Action(Action newAct)
    {
        Esc_button_Action += newAct;
    }
    Action Esc_button_Action;



    Action Space_button_Action;


    public void Add_Space_Action(Action newAct)
    {
        Space_button_Action += newAct;
    }





}
