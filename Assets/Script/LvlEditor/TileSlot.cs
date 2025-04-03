using UnityEngine;
using UnityEngine.EventSystems;

public class TileSlot : MonoBehaviour, IDropHandler
{
    public bool isGameWorldSlot = false;
    [SerializeField]GameObject CurrentItem;
    [SerializeField] bool IsTrashTile;
    public Vector2Int TileIndex;
    public void OnDrop(PointerEventData eventData)
    {
        if (IsTrashTile)
        {
            GameObject droppedPrefab = eventData.pointerDrag;
            Destroy(droppedPrefab);
        }
        if (isGameWorldSlot)
        {
            if (transform.childCount == 0)
            {
                GameObject droppedPrefab = eventData.pointerDrag;
                DraggablePrefab draggablePrefab = droppedPrefab.GetComponent<DraggablePrefab>();
                draggablePrefab.parentAfterDrag = transform;
            }
            else if (GetComponentInChildren<DraggablePrefab>() != null)
            {
                DraggablePrefab current = GetComponentInChildren<DraggablePrefab>();
                GameObject droppedPrefab = eventData.pointerDrag;
                DraggablePrefab draggablePrefab = droppedPrefab.GetComponent<DraggablePrefab>();
                if (current.isObject)
                {
                    if (draggablePrefab.isObject)
                    {
                        droppedPrefab = eventData.pointerDrag;
                        draggablePrefab.ResetPosistion();
                    }
                    else
                    {
                        droppedPrefab = eventData.pointerDrag;
                        draggablePrefab = droppedPrefab.GetComponent<DraggablePrefab>();
                        draggablePrefab.parentAfterDrag = transform;
                    }
                }
                else
                {
                    if (draggablePrefab.isObject)
                    {
                        droppedPrefab = eventData.pointerDrag;
                        draggablePrefab = droppedPrefab.GetComponent<DraggablePrefab>();
                        draggablePrefab.parentAfterDrag = transform;
                    }
                    else
                    {
                        droppedPrefab = eventData.pointerDrag;
                        draggablePrefab.ResetPosistion();
                    }
                }
            }
            else 
            {
                GameObject droppedPrefab = eventData.pointerDrag;
                DraggablePrefab draggablePrefab = droppedPrefab.GetComponent<DraggablePrefab>();
                draggablePrefab.ResetPosistion();
            }
        }
    }

    public void GenerateNewItem()
    {
        Instantiate(CurrentItem,transform);
    }
}
