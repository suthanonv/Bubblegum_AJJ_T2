using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Level_Section")]
public class Level_Section : ScriptableObject
{


    [SerializeField] bool _canPlayThisSection = false;

    public bool CanPlay => _canPlayThisSection;


    [SerializeField] List<SceneAsset> _scene_InSection = new List<SceneAsset>();


    List<Level_Info> _level_Info_List;

    public List<Level_Info> All_Level_Info => _level_Info_List;

    [SerializeField] int RequirementToClear = 2;

    [SerializeField] List<Level_Section> NextSection;

    private void OnValidate()
    {
        _level_Info_List.Clear();
        foreach (SceneAsset scene in _scene_InSection)
        {
            if (scene == null) continue;
            Level_Info level_Info = new Level_Info(scene, false);
            _level_Info_List.Add(level_Info);
        }
    }

    [ContextMenu("Set Level to Clear All")]
    void SetLevelClear()
    {
        foreach (Level_Info i in _level_Info_List)
        {
            i.SetNewState(true);
        }
    }

    public bool IsSceneInthisSection(string Name)
    {
        return _level_Info_List.FirstOrDefault(i => i._name == Name) != null;
    }

    public void UpdateSceneState(string name, bool State)
    {
        _level_Info_List.FirstOrDefault(i => i._name == name).SetNewState(State);
        isAllStateClear();
    }


    void isAllStateClear()
    {
        int count = 0;
        foreach (Level_Info i in _level_Info_List)
        {
            if (i.IsClear)
            {
                count++;
            }
        }

        if (count >= RequirementToClear)
        {
            UpdateSection();
        }
    }

    public void UpdateSection()
    {
        foreach (Level_Section i in NextSection)
        {
            i._canPlayThisSection = true;
        }
    }

}


[System.Serializable]
public class Level_Info
{
    public string _name;
    public SceneAsset SceneAsset { get; }
    public bool IsClear { get; private set; }

    public Level_Info(SceneAsset SceneAsset, bool State)
    {
        this.SceneAsset = SceneAsset;
        _name = SceneAsset.name;
        IsClear = State;
    }

    public void SetNewState(bool State)
    {
        IsClear = State;
    }
}
