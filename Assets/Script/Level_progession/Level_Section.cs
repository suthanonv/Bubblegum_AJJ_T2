using System.Collections.Generic;
using System.Linq;
using UnityEngine;



#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Level_Section")]
public class Level_Section : ScriptableObject
{
    [SerializeField] private bool _canPlayThisSection = false;
    public bool CanPlay => _canPlayThisSection;

    // Use SceneAsset in editor for convenience
#if UNITY_EDITOR
    [SerializeField] private List<SceneAsset> _sceneAssetsInSection = new List<SceneAsset>();
#endif

    // Scene names used in runtime
    [SerializeField] private List<int> _sceneIndexesInSection = new List<int>();

    private List<Level_Info> _level_Info_List;
    public List<Level_Info> All_Level_Info => Get_LevelInfo();

    [SerializeField] private int RequirementToClear = 2;
    [SerializeField] private List<Level_Section> NextSection;

    private List<Level_Info> Get_LevelInfo()
    {
        if (_level_Info_List == null || _level_Info_List.Count == 0)
        {
            SetUp();
        }

        return _level_Info_List;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _sceneIndexesInSection = _sceneAssetsInSection
            .Where(scene => scene != null)
            .Select(scene => GetBuildIndexByName(scene.name))
            .Where(index => index >= 0)
            .ToList();
    }

    // Helper method to get build index from scene name
    private int GetBuildIndexByName(string sceneName)
    {
        for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            string path = EditorBuildSettings.scenes[i].path;
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            if (name == sceneName)
                return i;
        }
        return -1; // Not found
    }
#endif

    [ContextMenu("Set Level to Clear All")]
    private void SetLevelClear()
    {
        foreach (Level_Info i in _level_Info_List)
        {
            i.SetNewState(true);
        }
    }

    private void SetUp()
    {
        _level_Info_List = new List<Level_Info>();
        foreach (int sceneName in _sceneIndexesInSection)
        {
            Level_Info level_Info = new Level_Info(sceneName, false);
            _level_Info_List.Add(level_Info);
        }
    }

    public bool IsSceneInthisSection(int name)
    {
        if (_sceneIndexesInSection.Contains(name)) return true;

        return false;
    }

    public Level_Info GetLevel(int name)
    {
        foreach (var sceneName in All_Level_Info)
        {
            if (sceneName.SceneName == name) return sceneName;
        }

        return null;
    }

    public void UpdateSceneState(int name, bool state)
    {
        var info = _level_Info_List.FirstOrDefault(i => i.SceneName == name);
        if (info != null)
        {
            info.SetNewState(state);
            CheckAllClear();
        }
    }

    private void CheckAllClear()
    {
        int clearCount = _level_Info_List.Count(i => i.IsClear);

        if (clearCount >= RequirementToClear)
        {
            UpdateSection();
        }
        else
        {
            SectionClear = false;
        }
    }

    public bool SectionClear { get; private set; }

    public void UpdateSection()
    {
        SectionClear = true;
        foreach (Level_Section section in NextSection)
        {
            section._canPlayThisSection = true;
        }
    }
}
[System.Serializable]
public class Level_Info
{
    public int SceneName { get; private set; }
    public bool IsClear { get; private set; }

    public Level_Info(int sceneName, bool isClear)
    {
        SceneName = sceneName;
        IsClear = isClear;
    }

    public void SetNewState(bool state)
    {
        IsClear = state;
    }
}
