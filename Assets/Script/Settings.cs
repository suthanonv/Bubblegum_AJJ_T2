using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Settings : MonoBehaviour
{

    [SerializeField] GameObject Gum;
    [SerializeField] EscMenuManager EscMenuManager;
    [SerializeField] Image returnButtonImage;
    [SerializeField] TMP_Text returnButtonText;
    [SerializeField] Slider[] sliders;
    Input_handle input;
    public int CurrentSelect;
    Color grey;
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
            CloseSettings();
        }
    }

    public void OpenSettings()
    {
        input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        input.gameObject.SetActive(false);
        Time.timeScale = 0;
        CurrentSelect = 1;
        this.gameObject.SetActive(true);
        grey = returnButtonImage.color;
        UpdateVisual();
    }

    public void CloseSettings()
    {
        input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        input.gameObject.SetActive(true);
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
        EscMenuManager.OpenEscMenu();

    }

    public void UpdateVisual()
    {
        if (CurrentSelect != 4)
        {
            returnButtonImage.color = grey;
            returnButtonText.fontStyle = TMPro.FontStyles.Normal;
            Gum.SetActive(true);
            RectTransform rt = Gum.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2 (rt.anchoredPosition.x, 340 - ((CurrentSelect - 1) * 190));
        }
        else 
        {
            Gum.SetActive(false);
            returnButtonImage.color = Color.black;
            returnButtonText.fontStyle = TMPro.FontStyles.Bold;
        }
    }

    void AdjustSetting(int index, int value)
    {
        sliders[index - 1].value += value * 0.05f;
    }
}
