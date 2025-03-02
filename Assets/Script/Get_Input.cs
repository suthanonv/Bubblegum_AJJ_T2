using UnityEngine;
using UnityEngine.InputSystem;

public class Get_Input : MonoBehaviour
{
    private Input_handle _input_Handle;
    private Vector2Int direction = Vector2Int.zero;
    private Vector2Int queuedDirection = Vector2Int.zero;
    [SerializeField]private float inputCooldown = 0.25f; 
    private float nextMoveTime = 0f; 

    [SerializeField] private InputAction playerControls;

    private void Awake()
    {
        SetUp();
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            Debug.LogError("PlayerControls is not assigned in Get_Input.");
            return;
        }

        playerControls.Enable();

        playerControls.performed -= PerformedMove;
        playerControls.canceled -= PerformedMove;

        playerControls.performed += PerformedMove;
        playerControls.canceled += PerformedMove;

        Debug.Log("OnMove successfully subscribed.");
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.performed -= PerformedMove;
            playerControls.canceled -= PerformedMove;
            playerControls.Disable();
        }
    }

    private void Update()
    {
        if (Time.time >= nextMoveTime && queuedDirection != Vector2Int.zero)
        {
            direction = queuedDirection;
            _input_Handle?.Calling(direction);
            nextMoveTime = Time.time + inputCooldown; 
            Debug.Log($"Direction updated: {direction}");
        }
    }

    void SetUp()
    {
        _input_Handle = GetComponent<Input_handle>();

        if (_input_Handle == null)
        {
            Debug.LogError("Input_handle component is missing from " + gameObject.name);
        }
    }

    public void PerformedMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.performed)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                queuedDirection = new Vector2Int(input.x > 0 ? 1 : -1, 0);
            }
            else
            {
                queuedDirection = new Vector2Int(0, input.y > 0 ? 1 : -1);
            }
            Debug.Log($"Queued Direction: {queuedDirection}");
        }
        else if (context.canceled)
        {
            queuedDirection = Vector2Int.zero;
            Debug.Log("Input Canceled, queued direction reset.");
        }
    }
}
