using UnityEngine;

[System.Serializable]
public class SaveObjectTile
{

    string _name;
    public string Name => _name;


    Vector2Int _tileIndex;


    public Vector2Int TileIndexID { get { return _tileIndex; } }
    int _tileTypeID;
    public int Tile_TypeID { get { return _tileTypeID; } }
    int _ocupiedObjectID;
    public int OcupiedTileID { get { return _ocupiedObjectID; } }


    public SaveObjectTile(string name, Vector2Int tileindex, int TileTypeID, int objectOnTileID)
    {
        _name = name;
        _tileIndex = tileindex;
        _tileTypeID = TileTypeID;
        _ocupiedObjectID = objectOnTileID;
    }


}
