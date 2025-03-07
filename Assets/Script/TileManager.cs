using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    List<GameObject> allTile = new List<GameObject>();
    List<float> allXcoordinate = new List<float> { };
    List<float> allYcoordinate = new List<float> { };
    float tileSizeX;
    float tileSizeY;

    GameObject[,] tileIndex = null;
    void Awake()
    {
        Tile[] tiles = GetComponentsInChildren<Tile>();
        foreach (Tile t in tiles)
        {
            allTile.Add(t.gameObject);
        }
        foreach (GameObject tile in allTile)
        {
            if (!allXcoordinate.Contains(tile.transform.position.x))
            {
                allXcoordinate.Add(tile.transform.position.x);
            }
            if (!allYcoordinate.Contains(tile.transform.position.y))
            {
                allYcoordinate.Add(tile.transform.position.y);
            }
        }
        allXcoordinate = allXcoordinate.OrderBy(x => x).ToList();
        allYcoordinate = allYcoordinate.OrderBy(y => y).ToList();
        tileIndex = new GameObject[allXcoordinate.Count, allYcoordinate.Count];
        tileSizeX = allXcoordinate[1] - allXcoordinate[0];
        tileSizeY = allYcoordinate[1] - allYcoordinate[0];
        foreach (GameObject tile in allTile)
        {
            tileIndex[(int)((tile.transform.position.x - allXcoordinate[0]) / tileSizeX), (int)((tile.transform.position.y - allYcoordinate[0]) / tileSizeY)] = tile;
            tile.GetComponent<Tile>().Tile_Index = new Vector2Int((int)((tile.transform.position.x - allXcoordinate[0]) / tileSizeX), (int)((tile.transform.position.y - allYcoordinate[0]) / tileSizeY));
        }
    }

    public Tile GetTile(Vector2Int index)
    {
        Debug.Log(index);
        return (tileIndex[index.x, index.y].GetComponent<Tile>());
    }
}
