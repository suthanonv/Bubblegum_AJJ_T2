using UnityEngine;
using UnityEngine.SceneManagement;
public class Input_handle : Singleton<Input_handle>
{
    private System.Action<Vector2Int> _input_action;
    private System.Action<string> _buttonPressedAction;
    private System.Action _registerStateAllCharacters;


    public void AddRegisterStateListener(System.Action action)
    {
        _registerStateAllCharacters += action;
    }

    public void AddMovementListener(System.Action<Vector2Int> action)
    {
        _input_action += action;
    }

    public void RemoveMovementListener(System.Action<Vector2Int> action)
    {
        _input_action -= action;
    }

    public void AddButtonListener(System.Action<string> action)
    {
        _buttonPressedAction += action;
    }



    public bool EnableMove { get; set; } = true;
    public void CallingMovement(Vector2Int Direction)
    {
        if (EnableMove == false) return;
        Debug.Log("Call Move");
        _registerStateAllCharacters.Invoke();
        _input_action?.Invoke(Direction);
    }


    public void CallingButtonPressed(string buttonName)
    {
        _buttonPressedAction?.Invoke(buttonName);
        Debug.Log($"CallingCommand {buttonName} from Scene {SceneManager.GetActiveScene().name}");
    }
}
