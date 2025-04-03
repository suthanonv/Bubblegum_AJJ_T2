using UnityEngine;

public class Input_handle : MonoBehaviour
{
    private System.Action<Vector2Int> _input_action;
    private System.Action<string> _buttonPressedAction;

    public void AddMovementListener(System.Action<Vector2Int> action)
    {
        _input_action += action;
    }

    public void AddButtonListener(System.Action<string> action)
    {
        _buttonPressedAction += action;
    }

    public void CallingMovement(Vector2Int Direction)
    {

        _input_action?.Invoke(Direction);
    }

    public void CallingButtonPressed(string buttonName)
    {
        _buttonPressedAction?.Invoke(buttonName);
    }
}
