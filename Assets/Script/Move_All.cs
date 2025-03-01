using System.Collections.Generic;
using UnityEngine;
public class Move_All : MonoBehaviour
{
    Grid_Manager gridManager;

    private void Start()
    {
        this.GetComponent<All_moveable_gum_holder>().Add_moveCall_Listener(MoveAll);
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }
    public void MoveAll(Vector2Int Direction, List<Movement> moveGum)
    {
        List<I_move> Validmove, Invalidmove;

        SetUp(Direction, moveGum, out Validmove, out Invalidmove);
        Move_vallid_Move(Direction, Validmove);

    }


    void SetUp(Vector2Int Direction, List<Movement> moveGum, out List<I_move> validMove, out List<I_move> InvalidMove)
    {
        validMove = new List<I_move>();
        InvalidMove = new List<I_move>();

        foreach (Movement m in moveGum)
        {
            List<I_move> moves = m.Canmove(Direction);
            if (moves.Count > 0)
            {
                foreach (Movement movement in moves)
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
        HashSet<I_move> validMoveSet = new HashSet<I_move>(Valid_Move);

        foreach (I_move move in validMoveSet)
        {
            move.Move(Direction);
        }
    }

    void Check_invalid_move(Vector2Int Direction, List<I_move> InValid_Move)
    {
        HashSet<I_move> validMoveSet = new HashSet<I_move>(InValid_Move);

        foreach (I_move move in validMoveSet)
        {
            Tile moveTile = gridManager.Get_Tile(Direction + move.PremovePosition(Direction));

            if (moveTile.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {

            }
            else
            {

            }
        }
    }
}
