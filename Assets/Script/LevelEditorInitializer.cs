using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelEditorInitializer : MonoBehaviour
{
    [SerializeField] PrefabID prefabID;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject canvas2;
    [SerializeField] GameObject GridManager;
    [SerializeField] GameObject ObjectFolder;
    [SerializeField] GameObject InputHandler;

    GameObject tileFolder;
    GameObject Objectfolder;
    GameObject IH;

    TileGen tileGen;

    public TileSlot[,] tileSlots = null;
    public float tileSize;
    Vector2 startingPos;
    bool XEven;
    bool YEven;
    public void Awake()
    {
        tileGen = FindFirstObjectByType<TileGen>();
    }
    public void CreateTileList(int X, int Y, float size)
    {
        tileSize = size;
        tileSlots = new TileSlot[X, Y];
        XEven = (X % 2 == 0);
        YEven = (Y % 2 == 0);
        if (XEven)
        {
            startingPos += new Vector2(((0 - (X / 2) - 0.5f) * tileSize), 0);
        }
        else
        {
            startingPos += new Vector2(0 - (X / 2 * tileSize), 0);
        }
        if (YEven)
        {
            startingPos += new Vector2(0, 0 - (((Y / 2) - 0.5f) * tileSize));
        }
        else
        {
            startingPos += new Vector2(0, 0 - (Y / 2 * tileSize));
        }
    }
    public void CreateLevel()
    {
        tileFolder = Instantiate(GridManager);
        Objectfolder = Instantiate(ObjectFolder);
        IH = Instantiate(InputHandler);
        foreach (var tile in tileSlots)
        {
            if (tile.gameObject.GetComponentInChildren<DraggablePrefab>() != null)
            {
                DraggablePrefab[] tiles = tile.gameObject.GetComponentsInChildren<DraggablePrefab>();
                foreach (var T in tiles)
                {
                    ID iD = T.GetComponentInChildren<ID>();
                    GameObject createdTiles = Instantiate(prefabID.GetPrefab(iD.id), new Vector3(startingPos.x + (tile.TileIndex.x * tileSize), startingPos.y + (tile.TileIndex.y * tileSize)), Quaternion.identity);
                    if (T.isObject)
                    {
                        createdTiles.transform.localScale = new Vector3(tileSize * 0.8f, tileSize * 0.8f);
                        createdTiles.transform.SetParent(Objectfolder.transform, false);
                    }
                    else
                    {
                        createdTiles.transform.localScale = new Vector3(tileSize, tileSize);
                        createdTiles.transform.SetParent(tileFolder.transform, false);
                    }
                }
            }
        }
        canvas.SetActive(false);
        canvas2.SetActive(true);

        Initializer.ExecuteInitialize();
    }

    public void Return()
    {
        Destroy(tileFolder);
        Destroy(Objectfolder);
        Destroy(IH);
        canvas.SetActive(true);
        canvas2.SetActive(false);
    }
}
