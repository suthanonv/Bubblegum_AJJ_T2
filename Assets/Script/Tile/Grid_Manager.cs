using System;
using UnityEngine;

public class Grid_Manager : MonoBehaviour, bbg_IInitialize
{

    public int InitializeLayer() => 0;
    public Action Initialize()
    {
        Action action = () => { Set_Func_Delegate(this.GetComponent<TileManager>().GetTile); };
        return action;
    }

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
