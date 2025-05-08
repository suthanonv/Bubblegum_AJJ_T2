using UnityEngine;
using UnityEngine.SceneManagement;

public class Load_Condition : MonoBehaviour
{
    [SerializeField] private Level_Section section;
    [SerializeField] private LevelLoader levelLoader;

    [Header("Scene References (Editor Only)")]
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset MenuScene;
    [SerializeField] private UnityEditor.SceneAsset Cinematic;
    [SerializeField] private UnityEditor.SceneAsset LevelSelectScene;
    [SerializeField] private UnityEditor.SceneAsset FirstLevelScene;
    [SerializeField] private UnityEditor.SceneAsset EndCreditScene;
#endif

    [Header("Scene Names (Used in Build)")]
    [SerializeField] private string MenuSceneName;
    [SerializeField] private string CinematicName;
    [SerializeField] private string LevelSelectSceneName;
    [SerializeField] private string FirstLevelSceneName;
    [SerializeField] private string EndCreditSceneName;

    private int _menuSceneIndex;
    private int _cinematicIndex;
    private int _levelSelectSceneIndex;
    private int _firstLevelSceneIndex;
    private int _endCreditSceneIndex;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (MenuScene != null) MenuSceneName = MenuScene.name;
        if (Cinematic != null) CinematicName = Cinematic.name;
        if (LevelSelectScene != null) LevelSelectSceneName = LevelSelectScene.name;
        if (FirstLevelScene != null) FirstLevelSceneName = FirstLevelScene.name;
        if (EndCreditScene != null) EndCreditSceneName = EndCreditScene.name;
    }
#endif

    private void Start()
    {
        _menuSceneIndex = Level_Progress_Manager.GetBuildIndexByName(MenuSceneName);
        _cinematicIndex = Level_Progress_Manager.GetBuildIndexByName(CinematicName);
        _levelSelectSceneIndex = Level_Progress_Manager.GetBuildIndexByName(LevelSelectSceneName);
        _firstLevelSceneIndex = Level_Progress_Manager.GetBuildIndexByName(FirstLevelSceneName);
        _endCreditSceneIndex = Level_Progress_Manager.GetBuildIndexByName(EndCreditSceneName);
    }

    public void LoadingType()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentIndex + 1;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        //  If current is second to last to next is end credit
        if (currentIndex == sceneCount - 2)
        {
            levelLoader.LoadLevel(_endCreditSceneIndex);
            return;
        }

        //  If on Main Menu
        if (currentIndex == _menuSceneIndex)
        {
            if (section.GetLevel(_firstLevelSceneIndex).IsClear)
            {
                levelLoader.LoadLevel(_levelSelectSceneIndex); // Continue
            }
            else
            {
                levelLoader.LoadLevel(_cinematicIndex); // New Game
            }
            return;
        }

        //  If section not yet cleareds
        if (!section.SectionClear)
        {
            if (section.IsSceneInthisSection(nextSceneIndex))
            {
                levelLoader.loadNextScene(); // Proceed to next
                return;
            }
            else
            {
                levelLoader.loadLevelSelectedScene(_levelSelectSceneIndex); // Back to select
                return;
            }
        }

        //  Section cleared to go to level select
        levelLoader.loadLevelSelectedScene(_levelSelectSceneIndex);
    }
}
