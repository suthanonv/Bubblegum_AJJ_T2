using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Condition : MonoBehaviour
{
    [SerializeField] private Level_Section section;
    [SerializeField] private LevelLoader levelLoader;

    public void LoadingType()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int currentIndex = Level_Progress_Manager.GetBuildIndexByName(SceneManager.GetActiveScene().name);

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
        int nextSceneName = currentIndex + 1;






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
