using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Grouping))]
public class Group_Moving : Base_Movement
{
    Grouping attached_obj;
    public override int Movelayer => -1;
    protected override void Start()
    {
        base.Start();
        attached_obj = this.GetComponent<Grouping>();
    }



    public override Vector2Int DefaultPreMove(Vector2Int Direction)
    {
        return mainComponent.Transform.currentTile_index + Direction;
    }

    public override List<Vector2Int> PremovePosition(Vector2Int Direction)
    {

        HashSet<Vector2Int> group_Info = new HashSet<Vector2Int>();
        Debug.Log(attached_obj.GetGroup().Count);
        foreach (Grouping group in attached_obj.GetGroup())
        {
            I_move get = group.GetComponent<I_move>();

            group_Info.Add(get.DefaultPreMove(Direction));
        }

        group_Info.Add(DefaultPreMove(Direction));

        pos = group_Info.ToList();


        if (Direction == new Vector2Int(0, 1)) pos = pos.OrderByDescending(i => i.y).ToList();
        else if (Direction == new Vector2Int(0, -1)) pos = pos.OrderBy(i => i.y).ToList();
        else if (Direction == new Vector2Int(1, 0)) pos = pos.OrderByDescending(i => i.x).ToList();
        else if (Direction == new Vector2Int(-1, 0)) pos = pos.OrderBy(i => i.x).ToList();
        return pos;

    }

}


