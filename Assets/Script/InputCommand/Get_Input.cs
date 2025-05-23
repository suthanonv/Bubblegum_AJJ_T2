using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Get_Input : Singleton<Get_Input>
{
    private Input_handle _inputHandle;
    private Vector2Int _direction = Vector2Int.zero;
    public Vector2Int direction => _direction;

    private Vector2Int queuedDirection = Vector2Int.zero;

    [SerializeField] private float inputCooldown = 0.25f;
    private float nextMoveTime = 0f;

    [SerializeField] private InputAction playerMovementControls;
    [SerializeField] private InputAction playerButtonPressed;

    public bool EnableInput = true;

    bool yes = false;

    protected override void Init()
    {
        Initialize();
    }

    public void Start()
    {

        if (playerMovementControls == null || playerButtonPressed == null)
        {
            Debug.LogError("Player movement or button input action is not assigned.");
            return;
        }

        playerMovementControls.Enable();
        playerButtonPressed.Enable();

        playerMovementControls.performed += OnMovePerformed;
        playerMovementControls.canceled += OnMovePerformed;
        playerButtonPressed.performed += OnButtonPressed;
        playerButtonPressed.canceled += OnButtonPressed;

        //Debug.Log("Input actions successfully subscribed.");
    }

    public void OnDestroy()
    {
        if (playerMovementControls != null)
        {
            playerMovementControls.performed -= OnMovePerformed;
            playerMovementControls.canceled -= OnMovePerformed;
            playerMovementControls.Disable();
        }

        if (playerButtonPressed != null)
        {
            playerButtonPressed.performed -= OnButtonPressed;
            playerButtonPressed.canceled -= OnButtonPressed;
            playerButtonPressed.Disable();
        }
    }

    private void Update()
    {
        if (yes)
        {
            inputBuffering();
        }
        else
        {
            StartCoroutine(WaitSec(0.3f));
        }
    }

    private void Initialize()
    {
        _inputHandle = GetComponent<Input_handle>();

        if (_inputHandle == null)
        {
            Debug.LogError($"Input_handle component is missing from {gameObject.name}");
        }
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (EnableInput == false) return;
        Vector2 input = context.ReadValue<Vector2>();

        if (context.performed)
        {
            queuedDirection = Mathf.Abs(input.x) > Mathf.Abs(input.y)
                ? new Vector2Int(input.x > 0 ? 1 : -1, 0)
                : new Vector2Int(0, input.y > 0 ? 1 : -1);

            //Debug.Log($"Queued Direction: {queuedDirection}");
        }
        else if (context.canceled)
        {
            queuedDirection = Vector2Int.zero;
            //Debug.Log("Input canceled, queued _direction reset.");
        }
    }

    private void OnButtonPressed(InputAction.CallbackContext context)
    {
        if (EnableInput == false) return;
        if (context.performed)
        {
            string buttonName = context.control.name;
            _inputHandle?.CallingButtonPressed(buttonName);
        }
    }
    private void inputBuffering()
    {
        if (EnableInput == false) return;
        if (Time.time >= nextMoveTime && queuedDirection != Vector2Int.zero)
        {
            _direction = queuedDirection;
            _inputHandle?.CallingMovement(_direction);
            nextMoveTime = Time.time + inputCooldown;
            //Debug.Log($"Direction updated: {_direction}");
        }
    }

    IEnumerator WaitSec(float time)
    {
        yield return new WaitForSeconds(time);
        yes = true;
    }
}
