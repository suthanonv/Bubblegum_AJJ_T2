using UnityEngine;
using UnityEngine.InputSystem;

public class Get_Input : MonoBehaviour
{
    private Input_handle _input_Handle;
    private Vector2Int direction = Vector2Int.zero;
    private Vector2Int queuedDirection = Vector2Int.zero;
    [SerializeField] private float inputCooldown = 0.25f;
    private float nextMoveTime = 0f;

    [SerializeField] private InputAction playerMovementControls;
    [SerializeField] private InputAction playerButtonPressed;

    private void Awake()
    {
        SetUp();
    }
    void SetUp()
    {
        _input_Handle = GetComponent<Input_handle>();

        if (_input_Handle == null)
        {
            Debug.LogError("Input_handle component is missing from " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        if (playerMovementControls == null || playerButtonPressed == null)
        {
            Debug.LogError("PlayerControls or ButtonPressed is not assigned in Get_Input.");
            return;
        }

        playerMovementControls.Enable();
        playerButtonPressed.Enable();

        playerMovementControls.performed -= PerformedMove;
        playerMovementControls.canceled -= PerformedMove;
        playerButtonPressed.performed -= PerformedButtonPress;

        playerMovementControls.performed += PerformedMove;
        playerMovementControls.canceled += PerformedMove;
        playerButtonPressed.performed += PerformedButtonPress;

        Debug.Log("OnMove and Button Press successfully subscribed.");
    }

    private void OnDisable()
    {
        if (playerMovementControls != null)
        {
            playerMovementControls.performed -= PerformedMove;
            playerMovementControls.canceled -= PerformedMove;
            playerMovementControls.Disable();
        }

        if (playerButtonPressed != null)
        {
            playerButtonPressed.performed -= PerformedButtonPress;
            playerButtonPressed.Disable();
        }
    }

    private void Update()
    {
        /*if (playerMovementControls != null)
        {
            Vector2 debugInput = playerMovementControls.ReadValue<Vector2>();
            if (debugInput != Vector2.zero)
            {
                Debug.Log($"Raw Input Detected: {debugInput}");
            }
        }*/

        if (Time.time >= nextMoveTime && queuedDirection != Vector2Int.zero)
        {
            direction = queuedDirection;
            _input_Handle?.CallingMovement(direction);
            nextMoveTime = Time.time + inputCooldown;
            Debug.Log($"Direction updated: {direction}");
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

    public void PerformedButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log($"Button {context} Pressed");
            _input_Handle?.CallingButtonPressed(); 
        }
    }
}
