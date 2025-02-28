using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, I_move
{
    Grid_Manager gridManager;
    [SerializeField] MainComponent mainComponent;
    private void Start()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }



    public Vector2Int PremovePosition(Vector2Int Direction)
    {
        return mainComponent.currentTile_index;
    }

    public List<I_move> Canmove(Vector2Int Direction)
    {
        List<I_move> Move = new List<I_move>();

        return Move; ;
    }

    public void Move(Vector2Int Direction)
    {

    }

    public void OnFinishMove()
    {

    }
}
