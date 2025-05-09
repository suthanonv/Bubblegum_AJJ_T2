using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject InputHandler; //for disabling player controls. TEMP FIX. CHANGE LATER!!!!
    [SerializeField] private GameObject EscapeMenu;
    [SerializeField] private GameObject[] EscapeMenuButtons;

    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;

    LevelLoader _levelLoader;

    bool onMainPanel = true;
    

    bool isPaused;

    private void Start()
    {
        _levelLoader = FindFirstObjectByType<LevelLoader>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log(name + ": Escape Key has been pressed.");
            if (!isPaused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
    }

    public void Pause()
    {
        isPaused = true;
        InputHandler.SetActive(false);

        OpenEscapeMenu();
    }

    public void Unpause()
    {
        if (onMainPanel)
        {
            InputHandler.SetActive(true);
            isPaused = false;
            CloseEscapeMenu();
        }
        else
        {
            ReturnToPreviousPanel();
        }
    }

    private void OpenEscapeMenu()
    {
        EscapeMenu.SetActive(true);
        Debug.Log(name + ": OpenEscapeMenu()");
    }
    private void CloseEscapeMenu()
    {
        EscapeMenu.SetActive(false);
        Debug.Log(name + ": CloseEscapeMenu()");
    }

    //What happens once a button in EscapeMenu has been pressed
    public void SettingsButton()
    {
        Debug.Log(name + ": SettingsButton pressed");
        CloseMainPanel();
        OpenSettingsPanel();
    }

    public void LevelSelectButton()
    {
        Debug.Log(name + ": LevelSelectButton pressed");
        CloseEscapeMenu();
        Unpause();
        _levelLoader.loadLevelSelectScene();
        
    }

    public void MainMenuButton()
    {
        Debug.Log(name + ": MainMenuButton pressed");
        CloseEscapeMenu();
        Unpause();
        _levelLoader.loadMainMenu();
        
    }

    public void ReturnToPreviousPanel()
    {
        if (SettingsPanel)
        {
            CloseSettingsPanel();
            OpenMainPanel();
        }
    }


    //Basic setActive/inactive methods
    private void OpenMainPanel()
    {
        MainPanel.SetActive(true);
        onMainPanel = true;
    }
    private void CloseMainPanel()
    {
        MainPanel.SetActive(false);
        onMainPanel = false;
    }

    private void OpenSettingsPanel()
    {
        SettingsPanel.SetActive(true);
    }
    private void CloseSettingsPanel()
    {
        SettingsPanel.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(EscapeMenuButtons[0]);
    }
}
