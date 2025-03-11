using System.Collections.Generic;
using UnityEngine;
public interface I_move
{
    Vector2Int PremovePosition(Vector2Int Direction, HashSet<I_move> visited = null);

    Vector2 DefaultPosition();

    List<I_move> Canmove(Vector2Int Direction, HashSet<I_move> visited = null);

    void Move(Vector2Int Direction);

    void OnFinishMove();
}
