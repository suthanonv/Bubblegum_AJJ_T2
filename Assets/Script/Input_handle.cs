using UnityEngine;

public class Input_handle : MonoBehaviour
{
    System.Action<Vector2Int> _input_action;


    public void AddListener(System.Action<Vector2Int> action)
    {
        _input_action += action;
    }

    public void Calling(Vector2Int Direction)
    {
        _input_action?.Invoke(Direction);
    }


}
