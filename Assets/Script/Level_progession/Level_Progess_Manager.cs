using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Progress_Manager : MonoBehaviour
{
    private static string FolderPath => Application.persistentDataPath;
    private const string FileName = "Level_Progress";

    private static Level_Progress_Manager _instance;
    public static Level_Progress_Manager Instance => Get_Instance();

    private All_Level_Progression levelProgress;

    public static Level_Progress_Manager Get_Instance()
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject("Level_Progress_Manager");
            _instance = obj.AddComponent<Level_Progress_Manager>();
            DontDestroyOnLoad(obj);
        }
        return _instance;
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (levelProgress == null)
            Load();
    }

    public void SetProgress(string levelName, bool state)
    {
        if (levelProgress == null)
        {
            Load();
        }

        levelProgress.SetLevelProgress(levelName, state);
    }

    public bool IsThisScenePassed(string levelName)
    {
        if (levelProgress == null)
        {
            Load();
        }

        return levelProgress.CanLoadThisScene(levelName);
    }

    [ContextMenu("Reset Progression")]
    private void ResetProgress()
    {
        levelProgress = new All_Level_Progression();
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void Load()
    {
        string path = Path.Combine(FolderPath, FileName + ".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            levelProgress = JsonUtility.FromJson<All_Level_Progression>(json);
        }
        else
        {
            levelProgress = new All_Level_Progression();
        }
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(levelProgress, true);
        File.WriteAllText(Path.Combine(FolderPath, FileName + ".json"), json);
    }
}

[Serializable]
public class All_Level_Progression
{
    private const int FirstLevelIndex = 2;

    [SerializeField]
    private List<LevelProgressEntry> entries = new List<LevelProgressEntry>();

    private Dictionary<string, bool> sceneProgress = new Dictionary<string, bool>();

    public void SetLevelProgress(string levelName, bool state)
    {
        if (!sceneProgress.ContainsKey(levelName))
        {
            AddNewScene(levelName, state);
        }
        else
        {
            SetNewState(levelName, state);
        }
    }

    public bool CanLoadThisScene(string levelName)
    {
        if (!sceneProgress.ContainsKey(levelName))
            return false;
        return sceneProgress[levelName];
    }

    private void AddNewScene(string levelName, bool state)
    {
        int buildIndex = SceneUtility.GetBuildIndexByScenePath(levelName);
        bool unlocked = buildIndex <= FirstLevelIndex || state;
        sceneProgress[levelName] = unlocked;
        entries.Add(new LevelProgressEntry(levelName, unlocked));
    }

    private void SetNewState(string levelName, bool state)
    {
        if (sceneProgress[levelName] == true && state == false) return;
        sceneProgress[levelName] = state;
        var entry = entries.Find(e => e.SceneName == levelName);
        if (entry != null) entry.State = state;
    }

    // Unity doesn't serialize Dictionary directly, so rebuild after loading
    public All_Level_Progression()
    {
        sceneProgress = new Dictionary<string, bool>();
    }

    public void OnAfterDeserialize()
    {
        sceneProgress = new Dictionary<string, bool>();
        foreach (var entry in entries)
        {
            sceneProgress[entry.SceneName] = entry.State;
        }
    }

    public void OnBeforeSerialize() { }
}

[Serializable]
public class LevelProgressEntry
{
    public string SceneName;
    public bool State;

    public LevelProgressEntry(string name, bool state)
    {
        SceneName = name;
        State = state;
    }
}
