using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Progress_Manager : MonoBehaviour
{
    private static Level_Progress_Manager _instance;
    public static Level_Progress_Manager Instance => _instance;

    [SerializeField] List<Level_Section> _base_Allsection = new List<Level_Section>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Optional
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        LoadData();
    }

    public void SaveNow() => Save();

    private void OnApplicationQuit()
    {
#if !UNITY_WEBGL
        Save();
#endif
    }

    void Save()
    {

    }

    [ContextMenu("Reset")]
    public void ResetProgess()
    {
        SaveData newData = new SaveData();
        ResetLevel();
        Save();
        LoadData();
    }

    private void ResetLevel()
    {
        foreach (var section in _base_Allsection)
        {
            section.ResetSection();
        }
    }

    void LoadData()
    {

    }

    private static Dictionary<string, int> sceneNameToIndex;

    public static void InitializeSceneLookup()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        sceneNameToIndex = new Dictionary<string, int>();

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);
            sceneNameToIndex[name] = i;
        }
    }

    public static int GetBuildIndexByName(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = Path.GetFileNameWithoutExtension(path);

            if (name == sceneName)
            {
                return i;
            }
        }

        Debug.LogWarning($"No Scene '{sceneName}' found in Build Settings!");
        return -1;
    }

    public void SetSceneState(int SceneName, bool State)
    {
        Level_Section section = GetSection(SceneName);

        if (section == null)
        {
            int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
            if (SceneName == lastSceneIndex)
            {
                Debug.Log($"[Level_Progress_Manager] Scene index {SceneName} is the final scene. No section found — skipping SetSceneState is allowed.");
                return;
            }

            Debug.LogWarning($"[Level_Progress_Manager] Could not find a Level_Section containing scene index {SceneName}.");
            return;
        }

        section.UpdateSceneState(SceneName, State);
    }

    public Level_Section GetSection(int SceneName)
    {
        foreach (var section in _base_Allsection)
        {
            if (section.IsSceneInthisSection(SceneName))
            {
                return section;
            }
        }

        return null;
    }

    [Serializable]
    public class SceneStatePair
    {
        public int SceneName;
        public bool Value;
    }

    [Serializable]
    public class SaveData
    {
        [SerializeField]
        private List<SceneStatePair> SceneNameToValueList = new List<SceneStatePair>();
        private Dictionary<int, bool> SceneNameToValue = new Dictionary<int, bool>();

        public void AddNewScene(int Name, bool Value)
        {
            SceneNameToValue[Name] = Value;

            var existing = SceneNameToValueList.Find(pair => pair.SceneName == Name);
            if (existing != null)
            {
                existing.Value = Value;
            }
            else
            {
                SceneNameToValueList.Add(new SceneStatePair { SceneName = Name, Value = Value });
            }
        }

        public void LoadSceneState(List<Level_Section> All_Level)
        {
            SceneNameToValue.Clear();
            foreach (var pair in SceneNameToValueList)
            {
                SceneNameToValue[pair.SceneName] = pair.Value;
            }

            foreach (Level_Section Section in All_Level)
            {
                foreach (Level_Info Level in Section.All_Level_Info)
                {
                    if (SceneNameToValue.ContainsKey(Level.SceneName))
                    {
                        Section.UpdateSceneState(Level.SceneName, SceneNameToValue[Level.SceneName]);
                    }
                }
            }
        }
    }
}
