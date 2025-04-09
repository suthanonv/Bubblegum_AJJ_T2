using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Wining_Check : MonoBehaviour
{
    List<Wining_Tile> wining_tiles = new List<Wining_Tile>();


    //bool Trigedred = false;

    private void Start()
    {
        FindAnyObjectByType<All_moveable_gum_holder>().OnFinishMove_AddListener(Check_isWining);
    }


    public void Add_WiningTile(Wining_Tile tile)
    {
        wining_tiles.Add(tile);
    }

    private bool _completedLevel = false;
    public bool completedLevel => _completedLevel; 

    public void Check_isWining()
    {
        bool isWin = true;

        foreach (var i in wining_tiles)
        {
            if (i.IsWin == false)
            {
                isWin = false; break;
            }
        }


        if (isWin)
        {
            if (completedLevel) return;
            _completedLevel = true;
        }
    }

    

}
