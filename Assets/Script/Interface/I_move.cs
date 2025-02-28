using System.Collections.Generic;
using UnityEngine;
public interface I_move
{
    Vector2Int PremovePosition(Vector2Int Direction);

    List<I_move> Canmove(Vector2Int Direction);

    void Move(Vector2Int Direction);


}
