using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : Singleton<LevelLoader>
{
    public event Action OnBeginLoad;
    public event Action OnEndLoad;

    GameObject cloud;
    UnityEngine.UI.Slider loadSceneProgressBar;
    GameObject loadScene;
    GameObject loadSceneCanva;
    //private bool LoadTimeChecker;


    protected override void Init()
    {
        base.Init();
        this.transform.parent = null;
    }




    public void LoadLevel(int levelNumber)
    {

        StartCoroutine(LoadScene(levelNumber));
    }
    public void loadNextScene()
    {

        int _currentLevel = SceneManager.GetActiveScene().buildIndex;

        //LoadLevel(_currentLevel + 1);

        StartCoroutine(ASyncLoadScene(_currentLevel + 1, 0.8f));
        //Debug.Log("load next scene");
    }
    public void loadPreviousScene()
    {

        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;



        //LoadLevel(_currentLevel - 1);
        StartCoroutine(ASyncLoadScene(_currentLevel - 1, 0.8f));
    }
    public void loadLevelSelectScene()
    {

        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;

        int levelLoaderIndex = Level_Progress_Manager.GetBuildIndexByName("Level Loader");

        //LoadLevel(_currentLevel - 1);
        StartCoroutine(ASyncLoadScene(levelLoaderIndex, 1.5f));
    }
    public void loadMainMenu()
    {
        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;

        int levelLoaderIndex = Level_Progress_Manager.GetBuildIndexByName("Main Menu");

        //LoadLevel(_currentLevel - 1);
        StartCoroutine(ASyncLoadScene(levelLoaderIndex, 1.5f));
    }
    public void reloadScene()
    {
        if (FindAnyObjectByType<Cloud>() != null) return;

        Debug.Log($"{this.gameObject.name}, reloadScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;

        StartCoroutine(ASyncLoadScene(_currentLevel, 0.8f));
    }
    public void loadLevelSelectedScene(int lvl)
    {

        //  Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;

        //LoadLevel(_currentLevel + 1);
        StartCoroutine(ASyncLoadScene(lvl, 1));
    }


    IEnumerator LoadScene(int SceneIndex)
    {
        AsyncOperation asyncload = SceneManager.LoadSceneAsync(SceneIndex);
        StartCoroutine(CheckLoadTime());
        while (!asyncload.isDone)
        {
            //loadSceneProgressBar.value = asyncload.progress;
            yield return null;
        }
        OnEndLoad?.Invoke();
    }

    IEnumerator ASyncLoadScene(int levelNumber, float duration)
    {
        OnBeginLoad?.Invoke();
        loadScene = FindAnyObjectByType<Sceneloader>(FindObjectsInactive.Include).gameObject;
        loadSceneCanva = loadScene.transform.parent.gameObject;
        loadSceneProgressBar = loadScene.GetComponentInChildren<UnityEngine.UI.Slider>(true);
        cloud = FindAnyObjectByType<Cloud>(FindObjectsInactive.Include).gameObject;
        cloud.SetActive(true);
        float time = 0;
        Vector2 startingPos = new Vector2(3520, 1980);
        while (time < duration)
        {
            time += Time.deltaTime;
            cloud.transform.localPosition = Vector2.Lerp(startingPos, Vector2.zero, time / duration);
            yield return null;
        }
        cloud.transform.localPosition = Vector2.zero;

        int sceneCount = SceneManager.sceneCountInBuildSettings;

        if (levelNumber >= sceneCount) levelNumber = 0;


        AsyncOperation asyncload = SceneManager.LoadSceneAsync(levelNumber);
        StartCoroutine(CheckLoadTime());
        while (!asyncload.isDone)
        {
            //loadSceneProgressBar.value = asyncload.progress;
            yield return null;
        }

        OnEndLoad?.Invoke();
        Get_Input.Instance.EnableInput = false;
        yield return new WaitForSeconds(0.05f);
        time = 0;
        startingPos = Vector2.zero;


        while (time < duration)
        {
            cloud.transform.localPosition = Vector2.Lerp(startingPos, new Vector2(-3520, -1980), time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        cloud.transform.localPosition = new Vector2(-3520, -1980);
        Get_Input.Instance.EnableInput = true;

        OnFinishCloud?.Invoke();
        Destroy(loadSceneCanva);

    }

    System.Action OnFinishCloud;

    public void Add_FinishCloudAnimationListener(System.Action Listener)
    {
        OnFinishCloud += Listener;
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




    public void AddSceneChangeEvent(I_SceneChange SceneChangeEvent)
    {
        OnBeginLoad += SceneChangeEvent.OnEndScene;
        OnEndLoad += SceneChangeEvent.OnStartScene;
        SceneChangeEvent.OnStartScene();
    }


}
