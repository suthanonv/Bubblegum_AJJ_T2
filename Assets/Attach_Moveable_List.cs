using System.Collections.Generic;
using UnityEngine;

public class Attach_Moveable_List : MonoBehaviour
{
    List<Attach_Moveable_List> push_able_List = new List<Attach_Moveable_List>();

    I_move this_move;

    public I_move Get_Move => this_move;
    private void Start()
    {
        this_move = GetComponent<I_move>();
        Reset_List();
    }

    public void Reset_List()
    {
        if (push_able_List.Count > 0)
        {
            foreach (Attach_Moveable_List i_Move in push_able_List)
            {
                i_Move.Reset_List();
            }
        }



        push_able_List = new List<Attach_Moveable_List>();
        push_able_List.Add(this);
    }

    public void Remove_Moveable(Attach_Moveable_List moveable)
    {
        push_able_List.Remove(moveable);
    }

    public void Add_New_Moveable(List<Attach_Moveable_List> newList)
    {
        foreach (Attach_Moveable_List move in newList)
        {
            if (push_able_List.Contains(move)) continue;
            push_able_List.Add(move);
        }

        List<Attach_Moveable_List> allList = new List<Attach_Moveable_List>();

        foreach (Attach_Moveable_List i in push_able_List)
        {
            foreach (Attach_Moveable_List e in push_able_List)
            {
                allList.Add(e);
            }
        }

        foreach (Attach_Moveable_List e in push_able_List)
        {
            e.Set_Same_list(push_able_List);
        }
    }

    public List<Attach_Moveable_List> Get_List() => push_able_List;


    public bool isElementInList(Attach_Moveable_List i)
    {
        return push_able_List.Contains(i);
    }

    public void Set_Same_list(List<Attach_Moveable_List> Origin)
    {
        push_able_List = Origin;
    }
}
