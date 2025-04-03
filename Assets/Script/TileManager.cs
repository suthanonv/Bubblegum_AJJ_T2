using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class TileManager : MonoBehaviour, IInitialize
{
    List<GameObject> allTile = new List<GameObject>();
    List<float> allXcoordinate = new List<float> { };
    List<float> allYcoordinate = new List<float> { };
    float tileSizeX;
    float tileSizeY;

    GameObject[,] tileIndex = null;
    void Awake()
    {
        if (tileIndex == null)
        {
            InitializeTiles();
        }
    }


    public int InitializeLayer() => 1;
    public Action Initialize()
    {
        Action action;

        action = () => { InitializeTiles(); };

        return action;
    }


    void InitializeTiles()
    {
        List<GameObject> allTile = new List<GameObject>();
        allXcoordinate = new List<float>();
        allYcoordinate = new List<float>();

        Tile[] tiles = GetComponentsInChildren<Tile>();
        foreach (Tile t in tiles)
        {
            allTile.Add(t.gameObject);
        }
        foreach (GameObject tile in allTile)
        {
            if (!allXcoordinate.Contains(tile.transform.localPosition.x))
            {
                allXcoordinate.Add(tile.transform.localPosition.x);
            }
            if (!allYcoordinate.Contains(tile.transform.localPosition.y))
            {
                allYcoordinate.Add(tile.transform.localPosition.y);
            }
        }
        allXcoordinate = allXcoordinate.OrderBy(x => x).ToList();
        allYcoordinate = allYcoordinate.OrderBy(y => y).ToList();
        tileIndex = new GameObject[allXcoordinate.Count, allYcoordinate.Count];
        tileSizeX = allXcoordinate[1] - allXcoordinate[0];
        tileSizeY = allYcoordinate[1] - allYcoordinate[0];
        foreach (GameObject tile in allTile)
        {
            tileIndex[(int)((tile.transform.localPosition.x - allXcoordinate[0]) / tileSizeX), (int)((tile.transform.localPosition.y - allYcoordinate[0]) / tileSizeY)] = tile;
            tile.GetComponent<Tile>().Tile_Index = new Vector2Int((int)((tile.transform.localPosition.x - allXcoordinate[0]) / tileSizeX), (int)((tile.transform.localPosition.y - allYcoordinate[0]) / tileSizeY));
            //Debug.Log($"{tile.name} : {new Vector2Int((int)((tile.transform.localPosition.x - allXcoordinate[0]) / tileSizeX), (int)((tile.transform.localPosition.y - allYcoordinate[0]) / tileSizeY))}");

        }
    }
    [ContextMenu("Reset Tile")]

    private void Reset()
    {
        List<GameObject> allTile = new List<GameObject>();
        allXcoordinate = new List<float>();
        allYcoordinate = new List<float>();
        tileIndex = null;
    }
    public Tile GetTile(Vector2Int index)
    {
        Debug.Log(tileIndex.Length);
        return (tileIndex[index.x, index.y].GetComponent<Tile>());
    }
}
