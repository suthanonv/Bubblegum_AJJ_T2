using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameDisplay : MonoBehaviour
{
    public TextMeshProUGUI sceneText;

    void Start()
    {
        if (sceneText != null)
        {
            string sceneName = SceneManager.GetActiveScene().name;
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            sceneText.text = $"{sceneName}";
        }
        else
        {
            Debug.LogWarning("SceneNameDisplay: TMP Text reference not set.");
        }
    }
}
