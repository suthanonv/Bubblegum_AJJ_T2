using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Movement_Assign))]
public class Movement : MonoBehaviour, I_move
{
    Grid_Manager gridManager;
    [SerializeField] MainComponent mainComponent;
    [SerializeField] UnityEvent OnMoveing = new UnityEvent();

    [SerializeField] UnityEvent OnfinishMove = new UnityEvent();
    private void Start()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }


    public Vector2 DefaultPosition() => mainComponent.Transform.currentTile_index;


    public Vector2Int PremovePosition(Vector2Int Direction, HashSet<I_move> visited = null)
    {


        Tile PreMove_Tile = gridManager.Get_Tile(mainComponent.Transform.currentTile_index + Direction);


        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();

        if (Moving_to_tile == null) return mainComponent.Transform.currentTile_index;

        if (Moving_to_tile.OcupiedObject == null) return mainComponent.Transform.currentTile_index + Direction;

        I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();

        if (Moveable_Object == null) return mainComponent.Transform.currentTile_index;


        if (Moveable_Object.Canmove(Direction).Count == 0) return mainComponent.Transform.currentTile_index;

        return mainComponent.Transform.currentTile_index + Direction;
    }

    public List<I_move> Canmove(Vector2Int Direction, HashSet<I_move> visited = null)
    {

        mainComponent.Transform.SetRotation(Direction);

        List<I_move> Move = new List<I_move>();

        if (PremovePosition(Direction) == mainComponent.Transform.currentTile_index) return Move;
        Move.Add(this);

        Tile PreMove_Tile = gridManager.Get_Tile(PremovePosition(Direction));
        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();

        if (Moving_to_tile.OcupiedObject == null) return Move;

        I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();


        foreach (I_move i in Moveable_Object.Canmove(Direction))
        {
            Move.Add(i);
        }


        return Move;
    }

    public void Move(Vector2Int Direction)
    {
        mainComponent.Transform.Position(Direction + mainComponent.Transform.currentTile_index, OnMove, OnFinishMove);
    }

    public void OnFinishMove()
    {
        Debug.Log("Begin Onfinishmove delegate of Movement");
        OnfinishMove.Invoke();
    }

    void OnMove()
    {
        OnMoveing.Invoke();
    }
}
