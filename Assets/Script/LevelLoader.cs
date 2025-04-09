using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private int _currentLevel;
    private int currentLevel => _currentLevel;
    private void Start()
    {
        _currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
    

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
    public void loadNextScene()
    {
        LoadLevel(_currentLevel + 1);
    }
    public void loadPreviousScene()
    {
        LoadLevel(_currentLevel - 1);
    }
    public void reloadScene()
    {
        LoadLevel(_currentLevel);
    }
}
