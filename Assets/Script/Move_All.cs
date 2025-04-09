using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class Move_All : MonoBehaviour, IInitialize
{
    Grid_Manager gridManager;

    private void Start()
    {
        this.GetComponent<All_moveable_gum_holder>().Add_moveCall_Listener(MoveAll);
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }


    public int InitializeLayer() => 1;
    public Action Initialize()
    {
        Action action;

        action = () => { Setting(); };

        return action;
    }

    void Setting()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }


    public void MoveAll(Vector2Int Direction, List<Movement> moveGum, Action CallBack)
    {
        List<I_move> Validmove, Invalidmove;

        SetUp(Direction, moveGum, out Validmove, out Invalidmove);
        Move_vallid_Move(Direction, Validmove);
        Interact_invalid_move(Direction, Invalidmove);
        CallBack?.Invoke();
    }


    public void SetUp(Vector2Int Direction, List<Movement> moveGum, out List<I_move> validMove, out List<I_move> InvalidMove)
    {
        validMove = new List<I_move>();
        InvalidMove = new List<I_move>();

        foreach (I_move m in moveGum)
        {


            List<I_move> moves = m.Canmove(Direction);


            if (moves.Count > 0)
            {
                foreach (I_move movement in moves)
                {
                    validMove.Add(movement);
                }
            }
            else
            {
                InvalidMove.Add(m);
            }
        }
    }
    void Move_vallid_Move(Vector2Int Direction, List<I_move> Valid_Move)
    {
        if (Direction == new Vector2Int(0, 1)) Valid_Move = Valid_Move.OrderByDescending(i => i.PremovePosition(Direction).y).ToList();
        else if (Direction == new Vector2Int(0, -1)) Valid_Move = Valid_Move.OrderBy(i => i.PremovePosition(Direction).y).ToList();
        else if (Direction == new Vector2Int(1, 0)) Valid_Move = Valid_Move.OrderByDescending(i => i.PremovePosition(Direction).x).ToList();
        else if (Direction == new Vector2Int(-1, 0)) Valid_Move = Valid_Move.OrderBy(i => i.PremovePosition(Direction).x).ToList();


        HashSet<I_move> validMoveSet = new HashSet<I_move>(Valid_Move);


        foreach (I_move move in validMoveSet)
        {

            move.Move(Direction);
        }
    }

    
    void Interact_invalid_move(Vector2Int Direction, List<I_move> InValid_Move)
    {
        HashSet<I_move> validMoveSet = new HashSet<I_move>(InValid_Move);

        foreach (I_move move in validMoveSet)
        {
            Tile moveTile = gridManager.Get_Tile(Direction + move.PremovePosition(Direction));
            MainComponent Main_Move_Object = gridManager.Get_Tile(move.PremovePosition(Direction)).GetComponent<MoveAble_Tile>().OcupiedObject;


            if (moveTile.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {
                if (tile.OcupiedObject.TryFindComponent_InChild<Object_Interactable>(out Object_Interactable Interact_Obj))
                {
                    Interact_Obj.Interact(Main_Move_Object);
                }
            }
            else
            {
                if (moveTile.gameObject.TryGetComponent<Object_Interactable>(out Object_Interactable Interact_Obj))
                {
                    Interact_Obj.Interact(Main_Move_Object);
                }
            }
        }
    }
}
