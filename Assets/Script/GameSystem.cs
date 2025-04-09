using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    [SerializeField] Get_Input get_Input;
    [SerializeField] GameObject inputGameObject;

    private void Start()
    {
        inputGameObject.SetActive(true);
        Debug.Log($"{this.gameObject.name} Set inputGameObject to true");
        
    }
    
}
