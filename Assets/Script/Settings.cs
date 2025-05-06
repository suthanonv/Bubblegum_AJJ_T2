using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class Settings : MonoBehaviour
{

    [SerializeField] GameObject Gum;
    [SerializeField] EscMenuManager EscMenuManager;
    [SerializeField] Image returnButtonColor;
    [SerializeField] Image returnButtonImage;
    [SerializeField] TMP_Text returnButtonText;
    [SerializeField] Slider[] sliders;
    Input_handle input;
    public int CurrentSelect;
    Color darkColor;
    Color lightColor;

    [SerializeField] bool isMainMenu;
    [SerializeField] MenuController controller;
    void Start()
    {

    }
    void Update()
    {
        if (CurrentSelect < 4 && Input.GetKeyDown(KeyCode.S))
        {
            CurrentSelect++;
            UpdateVisual();
        }
        if (CurrentSelect > 1 && Input.GetKeyDown(KeyCode.W))
        {
            CurrentSelect--;
            UpdateVisual();
        }
        if (CurrentSelect != 4 && Input.GetKeyDown(KeyCode.A))
        {
            AdjustSetting(CurrentSelect, -1);
        }
        if (CurrentSelect != 4 && Input.GetKeyDown(KeyCode.D))
        {
            AdjustSetting(CurrentSelect, 1);
        }
        if ((CurrentSelect == 4 && Input.GetKeyDown(KeyCode.Space)) || Input.GetKeyDown(KeyCode.Escape))
        {
            controller.ReturnToPreviousPanel();
        }
    }

    public void OpenSettings()
    {
        //lightColor = EscMenuManager.lightColor;
        //darkColor = EscMenuManager.darkColor;
        //input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        //input.gameObject.SetActive(false);
        //Time.timeScale = 0;
        //CurrentSelect = 1;
        //this.gameObject.SetActive(true);
        //UpdateVisual();
    }

    public void CloseSettings()
    {
        //input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        //input.gameObject.SetActive(true);
        //Time.timeScale = 1;
        //this.gameObject.SetActive(false);
        //if (!isMainMenu)
        //{
        //    EscMenuManager.OpenEscMenu();
        //}
    }

    public void UpdateVisual()
    {
        int positionAdjustment = 212;
        if (CurrentSelect != 4)
        {
            if(CurrentSelect == 1)
            {
                positionAdjustment = 212;
            }
            else if(CurrentSelect == 2)
            {
                positionAdjustment = 12;
            }
            else if( CurrentSelect == 3)
            {
                positionAdjustment = -190;
            }
            else
            {
                positionAdjustment = -1000;
            }
            returnButtonImage.color = lightColor;
            returnButtonText.fontStyle = TMPro.FontStyles.Normal;
            Gum.SetActive(true);
            RectTransform rt = Gum.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2 (rt.anchoredPosition.x, positionAdjustment);
        }
        else 
        {
            Gum.SetActive(false);
            returnButtonImage.color = darkColor;
            returnButtonText.fontStyle = TMPro.FontStyles.Bold;
        }
    }

    void AdjustSetting(int index, int value)
    {
        sliders[index - 1].value += value * 0.05f;
    }
}
