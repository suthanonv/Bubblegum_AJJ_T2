using System.Collections.Generic;
using UnityEngine;

public class Attach_Moveable_List : MonoBehaviour
{
    private List<Attach_Moveable_List> push_able_List = new List<Attach_Moveable_List>();
    private I_move this_move;

    public I_move Get_Move => this_move;


    [SerializeField] List<MainComponent> Group;

    List<Attach_Moveable_List> base_group = new List<Attach_Moveable_List>();

    private void Awake()
    {
        this_move = GetComponent<I_move>();
        BaseSetUp();
        Reset_List();
    }



    void BaseSetUp()
    {
        foreach (var item in Group)
        {
            if (item.TryFindComponent_InChild<Attach_Moveable_List>(out Attach_Moveable_List attachedd))
            {
                base_group.Add(attachedd);
            }
        }
    }

    void SetUP()
    {
        this.Add_New_Moveable(base_group);
    }

    public void Reset_List(HashSet<Attach_Moveable_List> visited = null)
    {
        if (visited == null)
            visited = new HashSet<Attach_Moveable_List>();

        // Prevent infinite recursion in case of cyclic references
        if (visited.Contains(this))
            return;

        visited.Add(this);

        // Create a copy to prevent modifying the list while iterating
        List<Attach_Moveable_List> tempList = new List<Attach_Moveable_List>(push_able_List);

        // Process children first
        foreach (Attach_Moveable_List i_Move in tempList)
        {
            foreach (Attach_Moveable_List i_ in i_Move.Get_List())
            {
                i_.Reset_List(visited);
            }
        }

        // Now reset the list
        push_able_List.Clear();

        push_able_List.Add(this);
        SetUP();
    }

    public void Remove_Moveable(Attach_Moveable_List moveable)
    {
        push_able_List.Remove(moveable);
    }

    public void Add_New_Moveable(List<Attach_Moveable_List> newList)
    {
        foreach (Attach_Moveable_List move in newList)
        {
            if (!push_able_List.Contains(move))
            {
                push_able_List.Add(move);
            }
        }

        // Instead of redundant loops, just make a copy of push_able_List
        List<Attach_Moveable_List> allList = new List<Attach_Moveable_List>(push_able_List);

        // Update each item with the new list
        foreach (Attach_Moveable_List e in push_able_List)
        {
            e.Set_Same_list(allList);
            Debug.Log("มึงนี่เองไอเหี้ย Add_New_Moveable ");
        }
    }

    public List<Attach_Moveable_List> Get_List() => new List<Attach_Moveable_List>(push_able_List);

    public bool isElementInList(Attach_Moveable_List i)
    {
        return push_able_List.Contains(i);
    }

    public void Set_Same_list(List<Attach_Moveable_List> Origin)
    {
        Debug.Log(Origin);
        push_able_List = new List<Attach_Moveable_List>(Origin);

    }
}
