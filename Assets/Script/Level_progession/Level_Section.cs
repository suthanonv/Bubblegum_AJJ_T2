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
    [SerializeField] private List<string> _sceneNamesInSection = new List<string>();

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
        _sceneNamesInSection = _sceneAssetsInSection
            .Where(scene => scene != null)
            .Select(scene => scene.name)
            .ToList();
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
        foreach (string sceneName in _sceneNamesInSection)
        {
            if (string.IsNullOrEmpty(sceneName)) continue;
            Level_Info level_Info = new Level_Info(sceneName, false);
            _level_Info_List.Add(level_Info);
        }
    }

    public bool IsSceneInthisSection(string name)
    {
        return _level_Info_List.FirstOrDefault(i => i.SceneName == name) != null;
    }

    public void UpdateSceneState(string name, bool state)
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
    public string SceneName { get; private set; }
    public bool IsClear { get; private set; }

    public Level_Info(string sceneName, bool isClear)
    {
        SceneName = sceneName;
        IsClear = isClear;
    }

    public void SetNewState(bool state)
    {
        IsClear = state;
    }
}
