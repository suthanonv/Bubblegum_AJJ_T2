using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Condition : MonoBehaviour
{
    [SerializeField] private Level_Section firstSection;
    [SerializeField] private LevelLoader levelLoader;

    [SerializeField] private int _menuScene_Index;
    [SerializeField] private int _levelScene_Index;
    [SerializeField] private int _firstLevel_Index;
    [SerializeField] private int _endCredit_Scene;

#if UNITY_EDITOR

    [SerializeField] private SceneAsset MenuScene;
    [SerializeField] private SceneAsset Level_Select_Scene;
    [SerializeField] private SceneAsset FirstLevelScene;
    [SerializeField] private SceneAsset EndCreditScene;

    private void OnValidate()
    {
        if (MenuScene == null || Level_Select_Scene == null || FirstLevelScene == null || EndCreditScene == null) return;

        _menuScene_Index = Level_Progress_Manager.GetBuildIndexByName(MenuScene.name);
        _levelScene_Index = Level_Progress_Manager.GetBuildIndexByName(Level_Select_Scene.name);
        _firstLevel_Index = Level_Progress_Manager.GetBuildIndexByName(FirstLevelScene.name);
        _endCredit_Scene = Level_Progress_Manager.GetBuildIndexByName(EndCreditScene.name);
    }
#endif

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

        // If current scene is the menu
        if (currentIndex == _menuScene_Index)
        {
            if (firstSection.GetLevel(_firstLevel_Index).IsClear)
            {
                levelLoader.LoadLevel(_levelScene_Index);
            }
            else
            {
                levelLoader.LoadLevel(_firstLevel_Index);
            }
            return;
        }

        // Check if next scene is in the same section
        int nextSceneIndex = currentIndex + 1;

        if (!firstSection.SectionClear)
        {
            if (firstSection.IsSceneInthisSection(nextSceneIndex))
            {
                levelLoader.loadNextScene();
            }
            else
            {
                levelLoader.loadLevelSelectedScene(_levelScene_Index);
            }
            return;
        }

        // Section cleared, go to level select
        levelLoader.loadLevelSelectedScene(_levelScene_Index);
    }
}
