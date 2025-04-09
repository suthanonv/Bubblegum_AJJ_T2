using UnityEngine;
using UnityEngine.SceneManagement;
public class Input_handle : MonoBehaviour
{
    private System.Action<Vector2Int> _input_action;
    private System.Action<string> _buttonPressedAction;
    private System.Action _registerStateAllCharacters;

    private void Start()
    {
    }

    public void AddRegisterStateListener(System.Action action)
    {
        _registerStateAllCharacters += action;
    }

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
        _registerStateAllCharacters.Invoke();
        _input_action?.Invoke(Direction);
    }


    public void CallingButtonPressed(string buttonName)
    {
        _buttonPressedAction?.Invoke(buttonName);
        Debug.Log($"CallingCommand {buttonName} from Scene {SceneManager.GetActiveScene().name}");
    }
}
