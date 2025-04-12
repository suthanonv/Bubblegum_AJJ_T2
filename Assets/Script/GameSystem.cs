using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    [SerializeField] GameObject inputGameObject;
    [SerializeField] Wining_Check wining_Check;
    [SerializeField] LevelLoader levelLoader;

    [SerializeField] private int sceneTransitionTime = 1;

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
    }
    private void HandleWin()
    {
        Debug.Log("[GameSystem] Handling win... loading next scene.");
        levelLoader.loadNextScene();
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
