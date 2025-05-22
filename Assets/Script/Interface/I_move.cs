using System;
using System.Collections.Generic;
using UnityEngine;


public interface I_move
{



    public Vector2Int GetDirection(Vector2Int Direction);

    Vector2Int DefaultPreMove(Vector2Int Direction);
    List<Vector2Int> PremovePosition(Vector2Int Direction);
    Vector2Int DefaultPosition();

    void Move(Vector2Int Direction, Action callBack = null);

    void OnInvalidMove();
}
