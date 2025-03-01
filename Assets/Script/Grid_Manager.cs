using System;
using UnityEngine;

public class Grid_Manager : MonoBehaviour
{

    private Func<Vector2Int, Tile> _getTile;

    private void Start()
    {
        Set_Func_Delegate(this.GetComponent<TileManager>().GetTile);
    }

    // Set the delegate function
    public void Set_Func_Delegate(Func<Vector2Int, Tile> get_Tile_Func)
    {
        _getTile = get_Tile_Func;
    }

    public Tile Get_Tile(Vector2Int index)
    {
        return _getTile?.Invoke(index);
    }
}
