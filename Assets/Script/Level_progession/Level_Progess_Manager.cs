using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Progress_Manager : MonoBehaviour
{
    private static Level_Progress_Manager _instance;
    public static Level_Progress_Manager Instance => _instance;

    [SerializeField] private List<Level_Section> _base_Allsection = new List<Level_Section>();

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
        SaveData newData = new SaveData();

        foreach (var section in _base_Allsection)
        {
            foreach (var level in section.All_Level_Info)
            {
                newData.AddNewScene(level.SceneName, level.IsClear);
            }
        }

        string json = JsonUtility.ToJson(newData, true);

#if UNITY_WEBGL
        PlayerPrefs.SetString("SaveProgress", json);
        PlayerPrefs.Save();
#else
        string path = Application.persistentDataPath + "/Progression.json";
        System.IO.File.WriteAllText(path, json);
#endif
    }

    [ContextMenu("Reset")]
    public void ResetProgress()
    {
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
        string json = "";

#if !UNITY_WEBGL
#else
        string path = Application.persistentDataPath + "/Progression.json";
        if (System.IO.File.Exists(path))
        {
            json = System.IO.File.ReadAllText(path);
        }
#endif

        if (!string.IsNullOrEmpty(json))
        {
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            loadedData.LoadSceneState(_base_Allsection);
        }
    }

    public static void InitializeSceneLookup()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        sceneNameToIndex = new Dictionary<string, int>();

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);
            sceneNameToIndex[name] = i;
        }
    }

    public static int GetBuildIndexByName(string sceneName)
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string name = System.IO.Path.GetFileNameWithoutExtension(path);

            if (name == sceneName)
                return i;
        }

        Debug.LogWarning($"[Level_Progress_Manager] Scene '{sceneName}' not found in Build Settings!");
        return -1;
    }

    public void SetSceneState(int sceneIndex, bool state)
    {
        Level_Section section = GetSection(sceneIndex);

        if (section == null)
        {
            int lastSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

            if (sceneIndex == lastSceneIndex)
            {
                Debug.Log($"[Level_Progress_Manager] Scene index {sceneIndex} is the final scene. Skipping SetSceneState.");
                return;
            }

            Debug.LogWarning($"[Level_Progress_Manager] No Level_Section found containing scene index {sceneIndex}.");
            return;
        }

        section.UpdateSceneState(sceneIndex, state);
    }

    public Level_Section GetSection(int sceneIndex)
    {
        foreach (var section in _base_Allsection)
        {
            if (section.IsSceneInthisSection(sceneIndex))
                return section;
        }

        return null;
    }

    private static Dictionary<string, int> sceneNameToIndex;

    [Serializable]
    public class SceneStatePair
    {
        public int SceneName;
        public bool Value;
    }

    [Serializable]
    public class SaveData
    {
        [SerializeField] private List<SceneStatePair> SceneNameToValueList = new List<SceneStatePair>();
        private Dictionary<int, bool> SceneNameToValue = new Dictionary<int, bool>();

        public void AddNewScene(int name, bool value)
        {
            SceneNameToValue[name] = value;

            var existing = SceneNameToValueList.Find(pair => pair.SceneName == name);
            if (existing != null)
                existing.Value = value;
            else
                SceneNameToValueList.Add(new SceneStatePair { SceneName = name, Value = value });
        }

        public void LoadSceneState(List<Level_Section> allLevels)
        {
            SceneNameToValue.Clear();
            foreach (var pair in SceneNameToValueList)
            {
                SceneNameToValue[pair.SceneName] = pair.Value;
            }

            foreach (var section in allLevels)
            {
                foreach (var level in section.All_Level_Info)
                {
                    if (SceneNameToValue.ContainsKey(level.SceneName))
                    {
                        section.UpdateSceneState(level.SceneName, SceneNameToValue[level.SceneName]);
                    }
                }
            }
        }
    }
}
