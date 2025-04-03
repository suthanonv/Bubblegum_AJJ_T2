using UnityEngine;

public class TileGen : MonoBehaviour
{
    [SerializeField] int X;
    [SerializeField] int Y;
    [SerializeField] float TileSize;
    [SerializeField] GameObject TileSlot;
    [SerializeField] Transform canvas;
    [SerializeField] LevelEditorInitializer LvlIni;
    Vector2 startingPos;
    bool XEven;
    bool YEven;
    void Start()
    {
        LvlIni.CreateTileList(X,Y,TileSize);
        startingPos = Vector2.zero;
        XEven = (X % 2 == 0);
        YEven = (Y % 2 == 0);
        TileSize *= 108f;
        if (XEven)
        {
            startingPos += new Vector2(-200 -(((X / 2) - 0.5f) * TileSize) , 0);
        }
        else 
        {
            startingPos += new Vector2(-200 - (X / 2 * TileSize),0);
        }
        if (YEven)
        {
            startingPos += new Vector2(0, 0 - (((Y / 2) - 0.5f) * TileSize));
        }
        else 
        {
            startingPos += new Vector2(0, 0 - (Y / 2 * TileSize));
        }
        for (int i = 0; i < X; i++)
        {
            for (int j = 0; j < Y; j++)
            {
                GameObject tileSlot = Instantiate(TileSlot, new Vector3(startingPos.x + (i * TileSize), startingPos.y + (j * TileSize)), Quaternion.identity);
                tileSlot.transform.SetParent(canvas, false);
                RectTransform rt = tileSlot.GetComponent<RectTransform>();
                rt.sizeDelta = new Vector2(TileSize, TileSize);
                TileSlot tile = tileSlot.GetComponent<TileSlot>();
                tile.isGameWorldSlot = true;
                tile.TileIndex = new Vector2Int(i, j);
                LvlIni.tileSlots[tile.TileIndex.x,tile.TileIndex.y] = tile;
            }
        }
    }

}
