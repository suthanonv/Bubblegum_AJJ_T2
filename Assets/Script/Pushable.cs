using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Attach_Moveable_List))]
public class Pushable : MonoBehaviour, I_move
{
    [SerializeField] MainComponent mainComponent;
    [SerializeField] UnityEvent OnfinishMove = new UnityEvent();
    Grid_Manager gridManager;

    Attach_Moveable_List attached_obj;

    public Vector2 DefaultPosition() => mainComponent.Transform.currentTile_index;

    void Start()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
        attached_obj = this.GetComponent<Attach_Moveable_List>();
    }

    public Vector2Int PremovePosition(Vector2Int Direction, HashSet<I_move> visited = null)
    {
        // Initialize visited set if it's the first call
        if (visited == null)
            visited = new HashSet<I_move>();

        // If this object has already been processed, return its default position to prevent infinite recursion
        if (visited.Contains(this))
            return mainComponent.Transform.currentTile_index;

        // Mark this object as visited
        visited.Add(this);

        // If no attached objects, return current position
        if (attached_obj.Get_List().Count == 0)
            return mainComponent.Transform.currentTile_index;

        Tile PreMove_Tile = gridManager.Get_Tile(mainComponent.Transform.currentTile_index + Direction);

        if (PreMove_Tile == null) // Safety check in case the tile does not exist
            return mainComponent.Transform.currentTile_index;

        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();

        if (Moving_to_tile == null)
            return mainComponent.Transform.currentTile_index;

        if (Moving_to_tile.OcupiedObject == null)
            return mainComponent.Transform.currentTile_index + Direction;

        I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();

        if (Moving_to_tile.OcupiedObject.TryFindComponent_InChild<Attach_Moveable_List>(out Attach_Moveable_List attached))
        {
            if (attached.isElementInList(attached_obj))
            {
                Debug.Log($"{gameObject.name} : enter state");

                // Prevent infinite recursion using visited set
                if (visited.Contains(attached.Get_Move))
                    return mainComponent.Transform.currentTile_index;

                if (attached.Get_Move.PremovePosition(Direction, visited) == attached.Get_Move.DefaultPosition())
                    return mainComponent.Transform.currentTile_index;

                return mainComponent.Transform.currentTile_index + Direction;
            }
        }

        if (Moveable_Object == null)
            return mainComponent.Transform.currentTile_index;

        // Prevent infinite recursion using visited set
        if (visited.Contains(Moveable_Object))
            return mainComponent.Transform.currentTile_index;

        if (Moveable_Object.PremovePosition(Direction, visited) == Moveable_Object.DefaultPosition())
            return mainComponent.Transform.currentTile_index;

        return mainComponent.Transform.currentTile_index + Direction;
    }

    public List<I_move> Canmove(Vector2Int Direction, HashSet<I_move> visited = null)
    {
        // If visited is null (first call), create a new HashSet
        if (visited == null)
            visited = new HashSet<I_move>();

        // If this object has already been processed, return an empty list to prevent infinite loops
        if (visited.Contains(this))
            return new List<I_move>();

        // Mark this object as visited
        visited.Add(this);

        List<I_move> Move = new List<I_move>();

        if (PremovePosition(Direction) == mainComponent.Transform.currentTile_index) return Move;
        if (attached_obj.Get_List().Count == 0) return Move;

        foreach (Attach_Moveable_List i in attached_obj.Get_List())
        {
            if (attached_obj == i) continue;
            if (i.Get_Move.PremovePosition(Direction) == i.Get_Move.DefaultPosition()) return Move;

            Move.Add(i.Get_Move);

            if (gridManager.Get_Tile(i.Get_Move.PremovePosition(Direction))
                .TryGetComponent<MoveAble_Tile>(out MoveAble_Tile move_tile))
            {
                if (move_tile.OcupiedObject != null)
                {
                    if (move_tile.OcupiedObject.TryFindComponent_InChild<I_move>(out I_move move))
                    {
                        // Use the visited set to prevent infinite recursion
                        Move.AddRange(move.Canmove(Direction, visited));
                    }
                }
            }
        }

        if (gridManager.Get_Tile(PremovePosition(Direction))
            .TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
        {
            if (tile.OcupiedObject != null)
            {
                if (tile.OcupiedObject.TryFindComponent_InChild<I_move>(out I_move move))
                {
                    // Use the visited set to prevent infinite recursion
                    Move.AddRange(move.Canmove(Direction, visited));
                }
            }
        }

        Move.Add(this);
        Debug.Log("Success");
        return Move;
    }

    public void Move(Vector2Int Direction)
    {
        mainComponent.Transform.Position(Direction + mainComponent.Transform.currentTile_index);
        OnFinishMove();
    }

    public void OnFinishMove()
    {
        OnfinishMove.Invoke();
    }
}
