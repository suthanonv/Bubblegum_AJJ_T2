using UnityEngine;

public class Get_Input : MonoBehaviour
{
    Input_handle _input_Handle;
    Vector2Int direction;


    void SetUp()
    {
        _input_Handle = this.GetComponent<Input_handle>();
    }


    void Update()
    {
        _input_Handle?.Calling(direction);
    }


}
