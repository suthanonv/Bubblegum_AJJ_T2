using UnityEngine;

public class TilePainter : MonoBehaviour
{
    GameObject CurrentGO;

    public void Paste(TileSlot tile)
    {
        Instantiate(CurrentGO,tile.transform);
    }

    public void ChangeCurrentG0(GameObject newGO)
    {
        CurrentGO = newGO;
    }
}
