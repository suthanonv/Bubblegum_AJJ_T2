using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SettingsButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] EscMenuManager m_MenuManager;
    [SerializeField] int myButton;
    bool isIn;

    void Update()
    {
        if (isIn)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                m_MenuManager.OnSelect();
            }
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        m_MenuManager.CurrentSelect = myButton;
        m_MenuManager.UpdateVisual(m_MenuManager.CurrentSelect);
        isIn = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        isIn = false;
    }
}
