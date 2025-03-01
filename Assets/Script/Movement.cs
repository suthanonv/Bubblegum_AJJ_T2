using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour, I_move
{
    Grid_Manager gridManager;
    [SerializeField] MainComponent mainComponent;
    [SerializeField] UnityEvent OnfinishMove = new UnityEvent();
    private void Start()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }



    public Vector2Int PremovePosition(Vector2Int Direction)
    {


        Tile PreMove_Tile = gridManager.Get_Tile(mainComponent.currentTile_index + Direction);


        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();

        if (Moving_to_tile == null) return mainComponent.currentTile_index;

        if (Moving_to_tile.OcupiedObject == null) return mainComponent.currentTile_index + Direction;

        I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();

        if (Moveable_Object == null) return mainComponent.currentTile_index;


        if (Moveable_Object.Canmove(Direction).Count == 0) return mainComponent.currentTile_index;

        return mainComponent.currentTile_index + Direction;
    }

    public List<I_move> Canmove(Vector2Int Direction)
    {
        List<I_move> Move = new List<I_move>();

        if (PremovePosition(Direction) == mainComponent.currentTile_index) return Move;

        Tile PreMove_Tile = gridManager.Get_Tile(PremovePosition(Direction));
        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();
        I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();


        foreach (I_move i in Moveable_Object.Canmove(Direction))
        {
            Move.Add(i);
        }

        Move.Add(this);

        return Move;
    }

    public void Move(Vector2Int Direction)
    {
        mainComponent.Position(Direction + mainComponent.currentTile_index);
        OnFinishMove();
    }

    public void OnFinishMove()
    {
        OnfinishMove.Invoke();
    }
}
