using UnityEngine;
using UnityEngine.EventSystems;

public class DraggablePrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public UnityEngine.UI.Image image;
    [SerializeField] public bool isObject;

    [HideInInspector] public Transform parentAfterDrag;
    Transform parentBeforeDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentBeforeDrag = transform.parent;
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        TileSlot tile = parentAfterDrag.gameObject.GetComponent<TileSlot>();
        if (tile.isGameWorldSlot)
        {
            TileSlot B4tile = parentBeforeDrag.gameObject.GetComponent<TileSlot>();
            if (!B4tile.isGameWorldSlot)
            {
                B4tile.GenerateNewItem();
            }
            transform.SetParent(parentAfterDrag);
            RectTransform rectTransform = GetComponent<RectTransform>();
            ResetPosistion();
        }
        else
        {
            transform.SetParent(parentBeforeDrag);
            ResetPosistion();
        }
    }

    public void ResetPosistion()
    {
        image.raycastTarget = true;
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Vector2.zero;
        RectTransform parentRT = transform.parent.gameObject.GetComponent<RectTransform>();
        if (isObject)
        {
            rectTransform.sizeDelta = parentRT.sizeDelta * 0.8f;
        }
        else 
        {
            rectTransform.sizeDelta = parentRT.sizeDelta;
        }
    }
}
