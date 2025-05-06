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
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }
        LoadData();
    }


    #region SaveLoad
    private void OnApplicationQuit()
    {
        Save();
    }

    void Save()
    {
        SaveData newData = new SaveData();

        foreach (var section in _base_Allsection)
        {
            foreach (var Level in section.All_Level_Info)
            {
                newData.AddNewScene(Level.SceneName, Level.IsClear);
            }
        }

        string json = JsonUtility.ToJson(newData, true);
        string path = Path.Combine(folderPath, "Progession.json");
        File.WriteAllText(path, json);
    }

    private static string folderPath => Application.persistentDataPath;


    public void ResetProgess()
    {
        SaveData newData = new SaveData();
        Save();
        LoadData();
    }

    void LoadData()
    {
        string path = Path.Combine(folderPath, "Progession" + ".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(json);
            loadedData.LoadSceneState(_base_Allsection);
        }
    }

    #endregion




    private static Dictionary<string, int> sceneNameToIndex;

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
            {
                Debug.Log($" Found scene '{sceneName}' at build index {i}");
                return i;
            }
        }

        Debug.LogWarning($" No Scene '{sceneName}' not found in Build Settings!");
        return -1;
    }


    public void SetSceneState(int SceneName, bool State)
    {
        GetSection(SceneName).UpdateSceneState(SceneName, State);
    }




    public Level_Section GetSection(int SceneName)
    {
        foreach (var section in _base_Allsection)
        {
            Debug.Log($"Comapred Input {SceneName} With {section.name}");
            if (section.IsSceneInthisSection(SceneName))
            {
                return section;
            }

        }


        return null;

    }


    [System.Serializable]
    public class SaveData
    {
        Dictionary<int, bool> SceneNameToValue = new Dictionary<int, bool>();

        public void AddNewScene(int Name, bool Value)
        {
            SceneNameToValue[Name] = Value;
        }

        public void LoadSceneState(List<Level_Section> All_Level)
        {
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


