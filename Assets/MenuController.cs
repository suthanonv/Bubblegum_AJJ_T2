using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    /// <important>
    /// Script By: Time (Mono) M
    /// To easily navigate this script, press 'ctrl + F' and type "////" to search through different sections
    /// Last Updated: 13th May, 2025
    /// <important>
    
    [Header("Input Handler")]
    [SerializeField] private GameObject InputHandler; //for disabling player controls. TEMP FIX. CHANGE LATER!!!!
    [Header("Escape Menu")]
    [SerializeField] private GameObject EscapeMenuObject;
    [SerializeField] private GameObject[] EscapeMenuButtons; //for use with Unity Input system
    //[SerializeField] private GameObject[] EscapeMenuPanels;

    [Header("Escape Menu Screens")]
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject SettingsPanel;

    LevelLoader _levelLoader;

    bool onMainPanel = true; //to allow for pressing escape in settings tabs to return to main escape menu
    

    bool isPaused;

    [Header("Settings Screen: Video")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    //// void Start
    private void Start()
    {
        //Find LevelLoader
        _levelLoader = FindFirstObjectByType<LevelLoader>();

        //Find Screen Resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> optionsList = new List<string>();

        int currentResolution = 0;
        bool currentResolutionChecked = false;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            Debug.Log(option);
            optionsList.Add(option);

            if (!currentResolutionChecked && (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height))
            {
                currentResolution = 1;
                currentResolutionChecked = true;
            }
        }

        resolutionDropdown.AddOptions(optionsList);
        resolutionDropdown.value = currentResolution;
        resolutionDropdown.RefreshShownValue();
    }

    //// void Update
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

    //// UI System Related
    private void OpenEscapeMenu()
    {
        EscapeMenuObject.SetActive(true);
        Debug.Log(name + ": OpenEscapeMenu()");
    }
    private void CloseEscapeMenu()
    {
        EscapeMenuObject.SetActive(false);
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
    } //Opens Escape Menu
    private void CloseMainPanel()
    {
        MainPanel.SetActive(false);
        onMainPanel = false;
    } //Closes Escape Menu

    private void OpenSettingsPanel() //Opens Options
    {
        SettingsPanel.SetActive(true);
    }
    private void CloseSettingsPanel() //Closes Options
    {
        SettingsPanel.SetActive(false);
    }


    //For swapping between pages
    [Header("Settings Pages")]
    public List<GameObject> SettingsMenuPanels = new List<GameObject>();
    public void Settings_SwitchPanel()//gets button pressed --> links to gameObject in list --> toggles active
    {
        GameObject clickedButton = EventSystem.current.currentSelectedGameObject;//to get child and not parent object
        int buttonId = (clickedButton.name[0] - '0') - 1;//Added -1 since Page '1' is actually index 0 in 

        //Debug.Log("Name of Button: " + clickedButton.name);
        //Debug.Log(name + ": buttonId = " + buttonId);

        Settings_TogglePanel(SettingsMenuPanels[Settings_GetActivePanel()], false);//get current panel --> toggle active
        Settings_TogglePanel(SettingsMenuPanels[buttonId], true);//toggle new panel
    }
    private int Settings_GetActivePanel()
    {
        foreach(GameObject g in SettingsMenuPanels)
        {
            if (g)
            {
                int index = SettingsMenuPanels.IndexOf(g);
                //Debug.Log(name + ": GetActivePanel(): Index = " + index);
                return index;
            }
        }
        return -99;
    }
    private void Settings_TogglePanel(GameObject panel, bool enable)//toggles panels
    {
        if (enable)//if active
        {
            panel.SetActive(true);
            //Debug.Log(name + ": Toggled Panel: " + panel.name + ", " + enable);
        }
        else if(!enable)
        {
            foreach (GameObject g in SettingsMenuPanels)//using this in case of panels that are accidentally open
            {
                if (g)
                {
                    g.SetActive(false);
                    //Debug.Log(name + ": Toggled Panel: " + panel.name + ", " + enable);
                }
            }
        }
        else
        {
            Debug.Log(name + " uhhhh something fucked up happened");
        }
    }


    ////Per-Page controls
    /// <summary>
    /// Page: Controls
    /// </summary>


    /// <summary>
    /// Page: Video
    /// </summary>
    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);//Screen.fullscreen is a bool so players SetFullscreen() take priority (setting resolutions shouldnt override that)
    }
    

    private void OnEnable() //For using keyboard controls in UI
    {
        EventSystem.current.SetSelectedGameObject(EscapeMenuButtons[0]);
    }
}
