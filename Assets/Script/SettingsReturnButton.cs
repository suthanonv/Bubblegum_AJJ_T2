using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsReturnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Settings settings;
    [SerializeField] int myButton;
    bool isIn;
    int tempLastButton;
    void Update()
    {
        if (isIn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                settings.CloseSettings();
            }
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        tempLastButton = settings.CurrentSelect;
        settings.CurrentSelect = myButton;
        settings.UpdateVisual();
        isIn = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        settings.CurrentSelect = tempLastButton;
        settings.UpdateVisual();
        isIn = false;
    }
}
