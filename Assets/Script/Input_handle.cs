using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Input_handle : MonoBehaviour
{
    private System.Action<Vector2Int> _input_action;
    private System.Action _buttonPressedAction; 

    public void AddMovementListener(System.Action<Vector2Int> action)
    {
        _input_action += action;
    }

    public void AddButtonListener(System.Action action)
    {
        _buttonPressedAction += action; 
    }

    public void CallingMovement(Vector2Int Direction)
    {
        _input_action?.Invoke(Direction);
    }

    public void CallingButtonPressed()
    {
        _buttonPressedAction?.Invoke(); 
    }
}
