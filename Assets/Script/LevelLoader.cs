using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public event Action OnBeginLoad;
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
        Destroy(this.gameObject);
    }
    public void loadNextScene()
    {

        Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        //LoadLevel(_currentLevel + 1);

        StartCoroutine(ASyncLoadScene(_currentLevel + 1, 0.8f));
        //Debug.Log("load next scene");
    }
    public void loadPreviousScene()
    {

        Debug.Log($"{this.gameObject.name}, loadPreviousScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");



        //LoadLevel(_currentLevel - 1);
        StartCoroutine(ASyncLoadScene(_currentLevel - 1, 0.8f));
    }
    public void reloadScene()
    {
        if (FindAnyObjectByType<Cloud>() != null) return;

        Debug.Log($"{this.gameObject.name}, reloadScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        StartCoroutine(ASyncLoadScene(_currentLevel, 0.8f));
    }
    public void loadLevelSelectedScene(int lvl)
    {

        //  Debug.Log($"{this.gameObject.name}, loadNextScene Set inputGameObject to false");
        int _currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (get_Input == null)
            Debug.LogWarning("[LevelLoader] get_Input is not assigned. Input will not be disabled.");

        //LoadLevel(_currentLevel + 1);
        StartCoroutine(ASyncLoadScene(lvl, 1));
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

        loadSceneComplete?.Invoke();
        newInput = FindAnyObjectByType<Get_Input>().gameObject;
        newInput.SetActive(false);
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
        newInput.SetActive(true);

        OnFinishCloud?.Invoke();
        Destroy(loadSceneCanva);
        Destroy(gameObject);
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

    public delegate void OnLoadSceneComplete();
    OnLoadSceneComplete loadSceneComplete;



    public void AddListener(OnLoadSceneComplete listener)
    {
        loadSceneComplete += listener;
    }


}
