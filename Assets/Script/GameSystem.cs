using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    private void Awake()
    {
        get_Input.OnEnable();
    }
}
