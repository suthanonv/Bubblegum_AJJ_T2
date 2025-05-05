using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    [SerializeField] GameObject inputGameObject;
    [SerializeField] Wining_Check wining_Check;
    [SerializeField] LevelLoader levelLoader;

    [SerializeField] private int sceneTransitionTime = 1;

    Load_Condition loadCondition;
    private void Start()
    {
        if (wining_Check != null)
        {
            wining_Check.OnWin += HandleWin;
        }
        else
        {
            Debug.LogError("[GameSystem] Wining_Check not assigned.");
        }

        loadCondition = FindAnyObjectByType<Load_Condition>();
    }
    private void HandleWin()
    {
        Level_Progress_Manager.Instance.SetSceneState(SceneManager.GetActiveScene().name, true);
        _transition.Invoke(loadNextScene);
    }


    System.Action<System.Action> _transition;

    public void SetHandleWinTransition(System.Action<System.Action> Transiton)
    {
        _transition = Transiton;
    }



    void loadNextScene()
    {
        Debug.Log("[GameSystem] Handling win... loading next scene.");

        loadCondition.LoadingType();

    }

    private void Update()
    {
        /*if (wining_Check.completedLevel)
        {
            StartCoroutine(delay());
            levelLoader.loadNextScene();
        }*/
    }



    IEnumerator delay()
    {
        yield return new WaitForSeconds(sceneTransitionTime);
    }
    private void OnDestroy()
    {
        if (wining_Check != null)
        {
            wining_Check.OnWin -= HandleWin;
        }
    }
}
