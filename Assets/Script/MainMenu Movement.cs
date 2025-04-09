using System.Collections;
using UnityEngine;

public class MainMenuMovement : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] Camera cam;
    bool startDrop;

    private void Update()
    {
        if(startDrop)
        {
            Drop();
        }
    }

    public void StartDrop()
    {
        startDrop = true;
        Dropper targetDrop = target.GetComponent<Dropper>();
        targetDrop.Drop();
    }
    public void Drop()
    {
        Vector3 screenPos = cam.WorldToScreenPoint(target.transform.position);
        Debug.Log($"{screenPos.x},{screenPos.y}");
        SetUIElementToScreenPosition(this.GetComponent<RectTransform>(), screenPos);
    }
    public void SetUIElementToScreenPosition(RectTransform uiElement, Vector2 screenPosition)
    {

        RectTransform parentRect = uiElement.parent as RectTransform;

        if (parentRect == null)
        {
            return;
        }
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRect, screenPosition,null , out localPoint))
        {
            uiElement.anchoredPosition = localPoint;
            Debug.Log($"{localPoint.x},{localPoint.y}");
        }
    }
}
