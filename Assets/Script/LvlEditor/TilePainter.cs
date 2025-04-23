using UnityEngine;

public class TilePainter : MonoBehaviour
{
    GameObject CurrentGO;
    [SerializeField]TileGen tilegen;

    public void Paste(TileSlot tile)
    {
        GameObject tilePasted = Instantiate(CurrentGO,tile.transform);
        DraggablePrefab DP = CurrentGO.GetComponent<DraggablePrefab>();
        RectTransform rectTransform = tilePasted.GetComponent<RectTransform>();
        RectTransform parentRT = tile.GetComponent<RectTransform>();
        if(DP.isObject)
        {
                rectTransform.sizeDelta = parentRT.sizeDelta * 0.8f;
        }
        else
        {
                rectTransform.sizeDelta = parentRT.sizeDelta;
        }
    }

    public void ChangeCurrentG0(GameObject newGO)
    {
        CurrentGO = newGO;
    }
}
