using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{

    static BackGroundMusic instance;
    [SerializeField] AudioClip Bgm;
    AudioSource _audioSouce;

    AudioClip _currentPlayedBGM;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (this.Bgm != null && instance != null)
            {
                if (_currentPlayedBGM != Bgm)
                {
                    PlayMusic(Bgm);
                }
            }
        }
    }

    private void Start()
    {
        _audioSouce = GetComponent<AudioSource>();
        _audioSouce.loop = true;
        PlayMusic(Bgm);
    }

    void PlayMusic(AudioClip Bgm)
    {
        _currentPlayedBGM = Bgm;
        _audioSouce.clip = Bgm;
        _audioSouce.Play();
    }



}
