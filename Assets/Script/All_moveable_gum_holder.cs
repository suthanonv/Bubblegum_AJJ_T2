using System;
using System.Collections.Generic;
using UnityEngine;
public class All_moveable_gum_holder : MonoBehaviour
{

    List<I_move> moveableGums = new List<I_move>();


    Action<Vector2Int, List<I_move>, Action> moveCall;

    Action _onfinishmove;

    private void Start()
    {
        FindAnyObjectByType<Input_handle>().AddMovementListener(MoveAll_call);
    }

    #region Add/remove moveable class




    public void Add_movealbe(Movement move)
    {
        moveableGums.Add(move);
    }

    public void Remove_moveable(Movement move)
    {
        moveableGums.Remove(move);
    }

    #endregion
    #region Adding/call move fucntion
    public void MoveAll_call(Vector2Int Direction)
    {
        moveCall?.Invoke(Direction, moveableGums, _onfinishmove);
    }
    public void Add_moveCall_Listener(Action<Vector2Int, List<I_move>, Action> newFunc)
    {
        moveCall += newFunc;
    }
    #endregion
    #region add CallBack
    public void OnFinishMove_AddListener(Action func)
    {
        _onfinishmove += func;
    }
    #endregion
}
