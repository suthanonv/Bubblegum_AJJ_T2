using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BackGroundMusic : MonoBehaviour
{

    static BackGroundMusic instance;

    [SerializeField] private List<BackGroundMusicAdjust> _musicAdjustList = new List<BackGroundMusicAdjust>();

    private AudioSource audioSource;
    private BackGroundMusicAdjust currentPlayedBGM = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    string _currentSceneName = "";
    private void Update()
    {
        if (_currentSceneName != SceneManager.GetActiveScene().name)
        {
            _currentSceneName = SceneManager.GetActiveScene().name;

            OnLoadNewScene();
        }
    }

    private void PlayMusic(BackGroundMusicAdjust musicData)
    {
        if (musicData == currentPlayedBGM) return;

        currentPlayedBGM = musicData;
        audioSource.clip = musicData.BGM;

        if (audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log($"Playing BGM: {audioSource.clip.name}");
        }
        else
        {
            Debug.LogWarning("BGM clip is missing in selected music data.");
        }
    }

    private void OnLoadNewScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        Debug.Log($"current scene name {currentSceneName}");
        foreach (var musicData in _musicAdjustList)
        {

            foreach (var scene in ListScenesInFolder(musicData.ScenePath))
            {
                Debug.Log($"scene {scene}");

                if (scene == currentSceneName)
                {
                    PlayMusic(musicData);
                    return;
                }
            }
        }

        // No match found – stop music
        audioSource.Stop();
        currentPlayedBGM = null;
        Debug.LogWarning("No matching music found for this scene.");
    }

    static List<string> ListScenesInFolder(string folderPath)
    {
        if (folderPath == null || folderPath == string.Empty) return new List<string>();

        // Specify your folder inside "Assets/" (example: "Assets/Scenes")
        folderPath = "Assets/Scenes";
        // Get all .unity files in the folder (recursive)
        string[] guids = AssetDatabase.FindAssets("t:Scene", new[] { folderPath });

        List<string> sceneNames = new List<string>();

        foreach (string guid in guids)
        {
            // Get the full asset path
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // Get the file name without extension
            string sceneName = Path.GetFileNameWithoutExtension(path);

            Debug.Log("Scene: " + sceneName);

            sceneNames.Add(sceneName);
        }

        return sceneNames;
    }

}
