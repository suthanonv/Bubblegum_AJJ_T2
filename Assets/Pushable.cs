using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour, I_move
{
    [SerializeField] MainComponent mainComponent;


    public Vector2Int PremovePosition(Vector2Int Direction)
    {
        return mainComponent.currentTile_index;
    }

    public List<I_move> Canmove(Vector2Int Direction)
    {
        List<I_move> Move = new List<I_move>();

        return Move;
    }

    public void Move(Vector2Int Direction)
    {

    }

}
