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
        inputGameObject.SetActive(true);
        Debug.Log($"{this.gameObject.name} Set inputGameObject to true");
        
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
}
