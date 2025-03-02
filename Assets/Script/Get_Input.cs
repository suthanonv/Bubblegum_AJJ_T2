using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Get_Input : MonoBehaviour
{
    private Input_handle _input_Handle;
    private Vector2Int direction = Vector2Int.zero;
    private Vector2Int storedDirection = Vector2Int.zero;
    private bool isHoldingKey = false;
    private Coroutine repeatCoroutine;

    [SerializeField] private InputAction playerControls;
    [SerializeField] private float initialDelay = 0.3f; 
    [SerializeField] private float repeatRate = 0.1f; 

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
        playerControls.canceled -= ReleasedMove;

        playerControls.performed += PerformedMove;
        playerControls.canceled += ReleasedMove;

        Debug.Log("OnMove successfully subscribed.");
    }

    private void OnDisable()
    {
        if (playerControls != null)
        {
            playerControls.performed -= PerformedMove;
            playerControls.canceled -= ReleasedMove;
            playerControls.Disable();
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

    public void PerformedMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if (context.performed)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                storedDirection = new Vector2Int(input.x > 0 ? 1 : -1, 0);
            }
            else
            {
                storedDirection = new Vector2Int(0, input.y > 0 ? 1 : -1);
            }

            if (!isHoldingKey)
            {
                isHoldingKey = true;
                direction = storedDirection; 
                if (repeatCoroutine != null) StopCoroutine(repeatCoroutine);
                repeatCoroutine = StartCoroutine(RepeatMovement());
            }

            Debug.Log($"Direction set to: {direction}");
        }
    }

    public void ReleasedMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isHoldingKey = false;
            direction = Vector2Int.zero;
            if (repeatCoroutine != null) StopCoroutine(repeatCoroutine);
            Debug.Log("Input Canceled, direction reset.");
        }
    }

    private IEnumerator RepeatMovement()
    {
        yield return new WaitForSeconds(initialDelay); 

        while (isHoldingKey)
        {
            direction = storedDirection;
            _input_Handle?.Calling(direction);
            Debug.Log($"Repeated Move: {direction}");
            yield return new WaitForSeconds(repeatRate); 
        }
    }
}
