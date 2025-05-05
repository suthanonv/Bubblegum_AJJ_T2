using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Condition : MonoBehaviour
{
    [SerializeField] private Level_Section section;
    [SerializeField] private LevelLoader levelLoader;

    public void LoadingType()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentIndex = SceneManager.GetActiveScene().buildIndex;

        // If current scene is the last one
        if (currentIndex == sceneCount - 1)
        {
            levelLoader.loadNextScene();
            return;
        }

        // If current scene is the first one
        if (currentIndex == 0)
        {
            if (section.SectionClear)
            {
                levelLoader.LoadLevel(1);
            }
            else
            {
                levelLoader.LoadLevel(2);
            }
            return;
        }

        // Check if next scene is in this section
        string nextSceneName = SceneUtility.GetScenePathByBuildIndex(currentIndex + 1);
        nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextSceneName);

        if (section.IsSceneInthisSection(nextSceneName))
        {
            levelLoader.loadNextScene();
        }
        else
        {
            levelLoader.loadLevelSelectedScene(1);
        }
    }

}
