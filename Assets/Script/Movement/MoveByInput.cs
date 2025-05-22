using UnityEngine;

public class MoveByInput : MonoBehaviour
{
    Base_Movement move;
    InputMove_Holder _moveHolder;

    [SerializeField] bool AddFromStart = false;

    void Awake()
    {
        _moveHolder = FindAnyObjectByType<InputMove_Holder>();
        move = GetComponent<Base_Movement>();

        if (AddFromStart) Add_move();
    }
    public void Add_move()
    {
        _moveHolder.Add_movealbe(move);
    }

    public void Remove_move()
    {
        Debug.Log("Removed Move");
        _moveHolder.Remove_moveable(move);
    }
}
