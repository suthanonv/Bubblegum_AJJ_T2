using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    [SerializeField] GameObject inputGameObject;
    GameObject newInput;
    GameObject cloud;
    UnityEngine.UI.Slider loadSceneProgressBar;
    GameObject loadScene;
    GameObject loadSceneCanva;
    //private bool LoadTimeChecker;
    private void Awake()
    {
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadLevel(int levelNumber)
    {
        inputGameObject.SetActive(false);
        SceneManager.LoadScene(levelNumber);
    }
    public void loadNextScene()
    {
        
        Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        //LoadLevel(_currentLevel + 1);
        StartCoroutine(ASyncLoadScene(_currentLevel + 1, 1));
    }
    public void loadPreviousScene()
    {
        
        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        //LoadLevel(_currentLevel - 1);
        StartCoroutine(ASyncLoadScene(_currentLevel - 1, 1));
    }
    public void reloadScene()
    {
        
        Debug.Log($"{this.gameObject.name}, reloadScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");


        //LoadLevel(_currentLevel);
        StartCoroutine(ASyncLoadScene(_currentLevel, 2));
    }
    public void loadLevelSelectedScene(int lvl)
    {

        Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        //LoadLevel(_currentLevel + 1);
        StartCoroutine(ASyncLoadScene(lvl, 1));
    }
    IEnumerator ASyncLoadScene(int levelNumber, int cloudSpeed)
    {
        loadScene = FindAnyObjectByType<Sceneloader>(FindObjectsInactive.Include).gameObject;
        loadSceneCanva = loadScene.transform.parent.gameObject;
        loadSceneProgressBar = loadScene.GetComponentInChildren<UnityEngine.UI.Slider>(true);
        cloud = FindAnyObjectByType<Cloud>(FindObjectsInactive.Include).gameObject;
        //LoadTimeChecker = false;
        //loadScene.SetActive(true);
        //loadSceneProgressBar.value = 0f;
        cloud.SetActive(true);
        for(int i = 0; i < 880/cloudSpeed; i++)
        {
            cloud.transform.position += new Vector3(-4f*cloudSpeed, -2.25f*cloudSpeed);
            yield return null;
        }
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(levelNumber);
        StartCoroutine(CheckLoadTime());
        while (!asyncload.isDone)
        {
            //loadSceneProgressBar.value = asyncload.progress;
            yield return null;
        }
        newInput = FindAnyObjectByType<Get_Input>().gameObject;
        newInput.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 880/cloudSpeed; i++)
        {
            cloud.transform.position += new Vector3(-4f * cloudSpeed, -2.25f * cloudSpeed);
            yield return null;
        }
        newInput.SetActive(true);
        Destroy(loadSceneCanva);
        Destroy(gameObject);
    }

    public IEnumerator CheckLoadTime(float countdownValue = 1.5f)
    {
        float currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSecondsRealtime(0.1f);
            currCountdownValue -= 0.1f;
        }
        //LoadTimeChecker = true;
    }
}
