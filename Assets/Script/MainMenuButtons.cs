using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text text;
    [SerializeField] MainMenuManager mainMenuManager;
    bool isIn;
    [SerializeField] int index;

    Load_Condition LoadCondition;


    private void Update()
    {
        if (isIn)
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (index == 0)
                {
                    mainMenuManager.StartGame();
                }
                else if (index == 1)
                {
                    mainMenuManager.OpenOptions();
                }
                else if (index == 2)
                {
                    mainMenuManager.ExitGame();
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontSize += 10;
        text.fontStyle = TMPro.FontStyles.Bold;
        isIn = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontSize -= 10;
        text.fontStyle = TMPro.FontStyles.Normal;
        isIn = false;
    }
}
