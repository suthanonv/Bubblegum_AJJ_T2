using System;
using System.Collections.Generic;
using UnityEngine;
public class All_moveable_gum_holder : MonoBehaviour
{

    List<Movement> moveableGums = new List<Movement>();


    Action<Vector2Int, List<Movement>> moveCall;

    private void Start()
    {
        FindAnyObjectByType<Input_handle>().AddMovementListener(MoveAll_call);
    }

    public void Add_movealbe(Movement move)
    {
        moveableGums.Add(move);
    }

    public void Remove_moveable(Movement move)
    {
        moveableGums.Remove(move);
    }

    public void MoveAll_call(Vector2Int Direction)
    {
        moveCall?.Invoke(Direction, moveableGums);
    }


    public void Add_moveCall_Listener(Action<Vector2Int, List<Movement>> newFunc)
    {
        moveCall += newFunc;
    }

}
