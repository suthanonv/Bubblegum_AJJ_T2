using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Base_Movement : MonoBehaviour, I_move
{
    public virtual int Movelayer => 0;

    protected Grid_Manager gridManager;
    [SerializeField] protected MainComponent mainComponent;
    [SerializeField] UnityEvent OnMoveing = new UnityEvent();
    [SerializeField] UnityEvent OnfinishMove = new UnityEvent();
    protected virtual void Start()
    {
        FindAnyObjectByType<InputMove_Holder>().OnFinishMove_AddListener(ResetOnEndFreame);
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }
    protected List<Vector2Int> pos = new List<Vector2Int>();

    public Vector2Int DefaultPosition() => mainComponent.Transform.currentTile_index;
    public MainComponent_Transform mainTransform => mainComponent.Transform;


    public virtual Vector2Int GetDirection(Vector2Int Direction)
    {
        mainTransform.SetRotation(Direction);
        return Direction;
    }


    public virtual Vector2Int DefaultPreMove(Vector2Int Direction)
    {
        return mainComponent.Transform.currentTile_index + Direction;
    }

    public virtual List<Vector2Int> PremovePosition(Vector2Int Direction)
    {
        pos = new List<Vector2Int>();
        pos.Add(mainComponent.Transform.currentTile_index + Direction);
        return pos;
    }

    public bool IsInSameList(Vector2Int nextPos)
    {
        return pos.Contains(nextPos);
    }


    public virtual void Move(Vector2Int Direction, Action callBack = null)
    {
        mainComponent.Transform.Position(Direction + mainComponent.Transform.currentTile_index, OnMove, OnFinishMove);
    }



    public void OnFinishMove()
    {
        OnfinishMove.Invoke();
    }

    public virtual void OnInvalidMove()
    {
        UnityEvent_onInvalidMove.Invoke();
        ResetOnEndFreame();
    }

    public virtual void SetRotation(Vector2Int Direction)
    {
        mainComponent.Transform.SetRotation(Direction);
    }


    [SerializeField] UnityEvent UnityEvent_onInvalidMove = new UnityEvent();


    protected void ResetOnEndFreame()
    {
        //Debug.Log("Bool Reset Freme");
    }

    void OnMove()
    {
        OnMoveing.Invoke();
    }
}
