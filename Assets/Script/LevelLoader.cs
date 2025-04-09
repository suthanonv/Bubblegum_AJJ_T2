using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    [SerializeField] GameObject inputGameObject;
    

    public void LoadLevel(int levelNumber)
    {
        SceneManager.LoadScene(levelNumber);
    }
    public void loadNextScene()
    {
        inputGameObject.SetActive(false);
        Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        LoadLevel(_currentLevel + 1);
    }
    public void loadPreviousScene()
    {
        inputGameObject.SetActive(false);
        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        LoadLevel(_currentLevel - 1);
    }
    public void reloadScene()
    {
        inputGameObject.SetActive(false);
        Debug.Log($"{this.gameObject.name}, reloadScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        LoadLevel(_currentLevel);
    }
}
