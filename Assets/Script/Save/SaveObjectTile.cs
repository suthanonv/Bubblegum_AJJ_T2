using UnityEngine;

[System.Serializable]
public class SaveObjectTile
{

    string _name;
    public string Name => _name;


    Vector2Int _tileIndex;


    public Vector2Int TileIndex { get { return _tileIndex; } }
    Tile _tileType;
    public Tile Tile_Type { get { return _tileType; } }
    MainComponent _ocupiedObject;
    public MainComponent OcupiedTile { get { return _ocupiedObject; } }


    public SaveObjectTile(string name, Vector2Int tileindex, Tile TileType, MainComponent objectOnTile)
    {
        _name = name;
        _tileIndex = tileindex;
        _tileType = TileType;
        _ocupiedObject = objectOnTile;
    }


}
