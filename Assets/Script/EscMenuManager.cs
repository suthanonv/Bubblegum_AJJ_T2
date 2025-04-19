using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscMenuManager : MonoBehaviour
{
    [SerializeField] GameObject Menu;
    [SerializeField] Settings Settings;
    [SerializeField] TMP_Text levelName; 
    LevelLoader levelLoader;
    Input_handle input;
    bool isOpen;
    public int CurrentSelect;
    Image[] imageList;
    TMP_Text[] textList;
    Color grey;
    void Start()
    {
        imageList = GetComponentsInChildren<Image>(true);
        textList = GetComponentsInChildren<TMP_Text>(true);
        grey = imageList[2].color;
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
       {
            if (isOpen)
            {
                CloseEscMenu();
            }
            else
            {
                OpenEscMenu();
            }
       }
       if (isOpen)
       {
            if (CurrentSelect < 5 && Input.GetKeyDown(KeyCode.S))
            {
                CurrentSelect++;
                UpdateVisual(CurrentSelect);
            }
            if (CurrentSelect > 1 && Input.GetKeyDown(KeyCode.W))
            {
                CurrentSelect--;
                UpdateVisual(CurrentSelect);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnSelect();
            }
        }
    }

    public void OpenEscMenu()
    {
        levelName.text = SceneManager.GetActiveScene().name;
        levelName.gameObject.SetActive(true);
        input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        input.gameObject.SetActive(false);
        Time.timeScale = 0;
        CurrentSelect = 1;
        isOpen = true;
        Menu.SetActive(true);
        UpdateVisual(CurrentSelect);
    }

    public void CloseEscMenu()
    {
        levelName.gameObject.SetActive(false);
        input = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        input.gameObject.SetActive(true);
        Time.timeScale = 1;
        isOpen = false;
        Menu.SetActive(false);
    }

    public void UpdateVisual(int Index)
    {
        List<int> list = new List<int> { 1, 2, 3, 4, 5 };

        list.RemoveAt(Index - 1);

        imageList[(Index * 2)].color = Color.black;

        textList[Index].fontStyle = TMPro.FontStyles.Bold;

        foreach (int i in list)
        {
            imageList[(i * 2)].color = grey;
            textList[i].fontStyle = TMPro.FontStyles.Normal;
        }
    }

    public void OnSelect()
    {
        levelLoader = FindAnyObjectByType<LevelLoader>();
        if (CurrentSelect == 1)
        {
            CloseEscMenu();
        }
        else if (CurrentSelect == 2)
        {
            levelLoader.reloadScene();
            CloseEscMenu();
        }
        else if (CurrentSelect == 3)
        {
            levelLoader.loadLevelSelectedScene(1);
            CloseEscMenu();
        }
        else if (CurrentSelect == 4)
        {
            OpenSetting();
        }
        else if (CurrentSelect == 5)
        {
            levelLoader.loadLevelSelectedScene(0);
            CloseEscMenu();
        }
    }

    public void OpenSetting()
    {
        CloseEscMenu();
        Settings.OpenSettings();
    }
}
