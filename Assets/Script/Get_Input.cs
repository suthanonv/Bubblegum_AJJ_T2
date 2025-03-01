using UnityEngine;
using UnityEngine.InputSystem;

public class Get_Input : MonoBehaviour
{
    private Input_handle _input_Handle;
    private Vector2Int direction;
    [SerializeField] private InputActionReference playerControls;

    private void Awake()
    {
        SetUp();
    }

    private void OnEnable()
    {
        if (playerControls == null || playerControls.action == null)
        {
            Debug.LogError("PlayerControls is not assigned or has no action in Get_Input.");
            return;
        }

        playerControls.action.Enable();

        playerControls.action.performed += OnMove;
        playerControls.action.canceled += OnMove;
    }

    private void OnDisable()
    {
        if (playerControls != null && playerControls.action != null)
        {
            playerControls.action.performed -= OnMove;
            playerControls.action.canceled -= OnMove;
            playerControls.action.Disable();
        }

    }

    private void Update()
    {
        _input_Handle?.Calling(direction);
    }

    void SetUp()
    {
        _input_Handle = GetComponent<Input_handle>();

        if (_input_Handle == null)
        {
            Debug.LogError("Input_handle component is missing from " + gameObject.name);
        }
    }


    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        direction = new Vector2Int(Mathf.RoundToInt(input.x), Mathf.RoundToInt(input.y));
        Debug.Log(direction);
    }
}
