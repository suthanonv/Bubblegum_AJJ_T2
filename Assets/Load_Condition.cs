using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Condition : MonoBehaviour
{
    [SerializeField] private Level_Section section;
    [SerializeField] private LevelLoader levelLoader;
    [SerializeField] private int sceneToendcredit;




    [SerializeField] SceneAsset MenuScene;
    int _menuScene_Index;

    [SerializeField] SceneAsset Level_Select_Scene;
    int _levelScene_Index;

    [SerializeField] SceneAsset FirstLevelScene;
    int _firstLevel_Index;

    [SerializeField] SceneAsset EndCreditScene;
    int _endCredit_Scene;

#if UNITY_EDITOR

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
        if (currentIndex == sceneCount)
        {
            levelLoader.loadNextScene();
            return;
        }

        // If current scene is the first one
        if (currentIndex == _menuScene_Index)
        {
            if (section.GetLevel(_firstLevel_Index).IsClear)
            {
                levelLoader.LoadLevel(_levelScene_Index);
            }
            else
            {
                levelLoader.LoadLevel(1);
            }
            return;
        }

        // Check if next scene is in this section
        int nextSceneName = currentIndex + 1;





        if (section.SectionClear == false)
        {
            if (section.IsSceneInthisSection(nextSceneName))
            {
                levelLoader.loadNextScene();
                return;
            }
            else
            {
                levelLoader.loadLevelSelectedScene(_levelScene_Index);
                return;
            }
        }

        levelLoader.loadLevelSelectedScene(_levelScene_Index);

    }

}
