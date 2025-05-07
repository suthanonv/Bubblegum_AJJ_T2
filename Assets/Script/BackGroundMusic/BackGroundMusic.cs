using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class BackGroundMusic : MonoBehaviour
{

    static BackGroundMusic instance;


    [SerializeField] AudioClip BGM_GamePlay;
    [SerializeField] AudioClip Bgm_EndCredit;

    private AudioSource audioSource;
    private AudioClip currentPlayedBGM = null;

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

    private void PlayMusic(AudioClip musicData)
    {
        if (musicData == currentPlayedBGM) return;

        currentPlayedBGM = musicData;
        audioSource.clip = musicData;

        if (audioSource.clip != null)
        {
            audioSource.Play();
            Debug.Log($"Playing BGM: {audioSource.clip.name}");
        }
        else
        {
            audioSource.Stop();
            Debug.LogWarning("BGM clip is missing in selected music data.");
        }
    }

    private void OnLoadNewScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        int sceneCount = SceneManager.sceneCountInBuildSettings;


        if (Level_Progress_Manager.GetBuildIndexByName(currentSceneName) == 0)
        {
            audioSource.Stop();
            currentPlayedBGM = null;
            return;
        }


        if (Level_Progress_Manager.GetBuildIndexByName(currentSceneName) == 1)
        {
            PlayMusic(BGM_GamePlay);
            return;
        }


        if (Level_Progress_Manager.GetBuildIndexByName(currentSceneName) == sceneCount - 1)
        {
            PlayMusic(Bgm_EndCredit);
            return;
        }



        PlayMusic(BGM_GamePlay);

    }

}
