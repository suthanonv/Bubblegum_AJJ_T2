using UnityEngine;

[System.Serializable]
public class SaveObjectTile
{
    Vector2Int _tileIndex;
    public Vector2Int TileIndex { get { return _tileIndex; } }
    Tile _tileType;
    public Tile Tile_Type { get { return _tileType; } }
    MainComponent _ocupiedObject;
    public MainComponent OcupiedTile { get { return _ocupiedObject; } }


    public SaveObjectTile(Vector2Int tileindex, Tile TileType, MainComponent objectOnTile)
    {
        _tileIndex = tileindex;
        _tileType = TileType;
        _ocupiedObject = objectOnTile;
    }


}
