using System;
using System.Collections.Generic;
using UnityEngine;
public class StraightMove : Base_Movement
{
    public override int Movelayer => 1;

    [SerializeField] Direction MoveDirection;
    Vector2Int _Movedirection;
    MoveObject_Manager Movemanager;
    protected override void Start()
    {
        base.Start();
        Movemanager = FindAnyObjectByType<MoveObject_Manager>();
        _Movedirection = All_Direction.Directions[(int)MoveDirection];

        SetRotation(_Movedirection);
    }

    public override Vector2Int DefaultPreMove(Vector2Int Direction)
    {
        return mainComponent.Transform.currentTile_index + _Movedirection;
    }

    public override Vector2Int GetDirection(Vector2Int Direction)
    {
        return _Movedirection;
    }

    public override List<Vector2Int> PremovePosition(Vector2Int Direction)
    {
        List<Vector2Int> preMove = new List<Vector2Int>();

        preMove.Add(mainComponent.Transform.currentTile_index + _Movedirection);

        return preMove;
    }

    public override void SetRotation(Vector2Int Direction)
    {
        mainComponent.Transform.SetRotation(_Movedirection);
    }

    public override void Move(Vector2Int Direction, Action callBack = null)
    {
        base.Move(_Movedirection);
    }

    public override void OnInvalidMove()
    {
        base.OnInvalidMove();
        Debug.Log("OnInvalid Move");
        _Movedirection *= -1;


    }

}
