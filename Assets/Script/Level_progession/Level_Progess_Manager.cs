using System.Collections.Generic;
using System.IO;
using UnityEngine;

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
    }

    private static string folderPath => Application.persistentDataPath;

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


    const string ScriptAbleObject_FilePath = "Assets/Scenes/Playable Prototype/Section_Manage";


    public void SetSceneState(int SceneName, bool State)
    {
        GetSection(SceneName).UpdateSceneState(SceneName, State);
    }




    public Level_Section GetSection(int SceneName)
    {
        Debug.Log(SceneName);


        foreach (var section in _base_Allsection)
        {
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


