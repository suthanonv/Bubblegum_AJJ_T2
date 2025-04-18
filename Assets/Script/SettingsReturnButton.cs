using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsReturnButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Settings settings;
    [SerializeField] int myButton;
    bool isIn;

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
        settings.CurrentSelect = myButton;
        settings.UpdateVisual();
        isIn = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        isIn = false;
    }
}
