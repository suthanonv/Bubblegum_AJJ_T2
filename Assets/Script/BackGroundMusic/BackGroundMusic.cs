using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BackGroundMusic : MonoBehaviour
{
    static BackGroundMusic instance;

    [SerializeField] private List<BackGroundMusicAdjust> _musicAdjustList = new List<BackGroundMusicAdjust>();

    private AudioSource audioSource;
    private BackGroundMusicAdjust currentPlayedBGM = null;
    private string _currentSceneName = "";

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

    private void Update()
    {
        string activeScene = SceneManager.GetActiveScene().name;
        if (_currentSceneName != activeScene)
        {
            _currentSceneName = activeScene;
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
        Debug.Log($"Current scene name: {currentSceneName}");

        foreach (var musicData in _musicAdjustList)
        {
            if (musicData.SceneNames.Contains(currentSceneName))
            {
                PlayMusic(musicData);
                return;
            }
        }

        // No match found – stop music
        audioSource.Stop();
        currentPlayedBGM = null;
        Debug.LogWarning("No matching music found for this scene.");
    }
}
