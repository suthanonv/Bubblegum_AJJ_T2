using UnityEngine;

public class Temp_Get_Input : MonoBehaviour
{
    private Input_handle _input_Handle;
    private Vector2Int direction;
    void Start()
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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            direction = new Vector2Int(0, 1);
            _input_Handle.CallingMovement(direction);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            direction = new Vector2Int(0, -1);

            _input_Handle.CallingMovement(direction);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            direction = new Vector2Int(1, 0);


            _input_Handle.CallingMovement(direction);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {

            direction = new Vector2Int(-1, 0);

            _input_Handle.CallingMovement(direction);
            return;
        }
    }
}
