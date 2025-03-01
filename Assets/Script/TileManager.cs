using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TileManager : MonoBehaviour
{
    List<GameObject> allTile = new List<GameObject>();
    List<float> allXcoordinate = new List<float> { };
    List<float> allYcoordinate = new List<float> { };
    [SerializeField]
    float tileSize;
    [SerializeField]
    GameObject[,] tileIndex = null;
    void Start()
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
        tileIndex = new GameObject[allXcoordinate.Count,allYcoordinate.Count];
        tileSize = allXcoordinate[1] - allXcoordinate[0];
        foreach (GameObject tile in allTile)
        {
            tileIndex[(int)((tile.transform.position.x - allXcoordinate[0]) / tileSize), (int)((tile.transform.position.y - allYcoordinate[0]) / tileSize)] = tile;
        }
    }

    public Tile GetTile(Vector2Int index)
    {
        return (tileIndex[index.x, index.y].GetComponent<Tile>());
    }
}
