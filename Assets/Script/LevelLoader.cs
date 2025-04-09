using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;



    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
    public void loadNextScene()
    {
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        LoadLevel(_currentLevel + 1);
    }
    public void loadPreviousScene()
    {
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        LoadLevel(_currentLevel - 1);
    }
    public void reloadScene()
    {
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        LoadLevel(_currentLevel);
    }
}
