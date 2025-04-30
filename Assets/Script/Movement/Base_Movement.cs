using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Base_Movement : MonoBehaviour, I_move
{
    protected Grid_Manager gridManager;
    [SerializeField] protected MainComponent mainComponent;
    [SerializeField] UnityEvent OnMoveing = new UnityEvent();

    [SerializeField] UnityEvent OnfinishMove = new UnityEvent();
    protected virtual void Start()
    {
        FindAnyObjectByType<All_moveable_gum_holder>().OnFinishMove_AddListener(ResetOnEndFreame);
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }


    public Vector2 DefaultPosition() => mainComponent.Transform.currentTile_index;




    Vector2Int _CalculatedPrePos = new Vector2Int(-10000, -10000);

    public Vector2Int PremovePosition(Vector2Int Direction, HashSet<I_move> visited = null)
    {
        if (_CalculatedPrePos != new Vector2Int(-10000, -10000)) return _CalculatedPrePos;


        _CalculatedPrePos = Check_PremovePosition(Direction, visited);
        return _CalculatedPrePos;
    }



    public virtual Vector2Int Check_PremovePosition(Vector2Int Direction, HashSet<I_move> visited = null)
    {
        Tile PreMove_Tile = gridManager.Get_Tile(mainComponent.Transform.currentTile_index + Direction);
        MoveAble_Tile Moving_to_tile = PreMove_Tile.GetComponent<MoveAble_Tile>();
        if (Moving_to_tile == null) return mainComponent.Transform.currentTile_index;
        if (Moving_to_tile.CanMove(mainComponent) == false) return mainComponent.Transform.currentTile_index;
        if (Moving_to_tile.OcupiedObject != null)
        {
            I_move Moveable_Object = Moving_to_tile.OcupiedObject.FindComponnet_InChild<I_move>();
            if (Moveable_Object == null) return mainComponent.Transform.currentTile_index;
            if (Moveable_Object.Canmove(Direction).Count == 0) return mainComponent.Transform.currentTile_index;
        }


        return mainComponent.Transform.currentTile_index + Direction;
    }

    bool Cal_Canmove = false;

    List<I_move> Cal_MoveList = new List<I_move>();

    public List<I_move> Canmove(Vector2Int Direction, HashSet<I_move> visited = null)
    {
        if (Cal_Canmove) return Cal_MoveList;

        Cal_MoveList = Check_Canmove(Direction, visited);
        Cal_Canmove = true;
        return Cal_MoveList;
    }




    public virtual List<I_move> Check_Canmove(Vector2Int Direction, HashSet<I_move> visited = null)
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
        OnfinishMove.Invoke();
    }

    public void OnInvalidMove()
    {

        ResetOnEndFreame();
    }


    void ResetOnEndFreame()
    {
        _CalculatedPrePos = new Vector2Int(-10000, -10000);
        Cal_Canmove = false;
        Debug.Log("Bool Reset Freme");
    }

    void OnMove()
    {
        OnMoveing.Invoke();
    }
}
