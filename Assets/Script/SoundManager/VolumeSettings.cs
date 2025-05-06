using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider MasterSlider;

    const string MIXER_MUSIC = "MusicVolumn";
    const string MIXER_SFX = "SFXVolumn";
    const string MIXER_MASTER = "MASTERVolumn";

    const string PREF_MUSIC = "MusicVolume";
    const string PREF_SFX = "SFXVolume";
    const string PREF_MASTER = "MasterVolume";

    private void Awake()
    {
        // Load saved values


        // Set listeners
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        MasterSlider.onValueChanged.AddListener(SetMasterVolume);
    }

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat(PREF_MUSIC, 0.75f);
        sfxSlider.value = PlayerPrefs.GetFloat(PREF_SFX, 0.75f);
        MasterSlider.value = PlayerPrefs.GetFloat(PREF_MASTER, 0.75f);

        // Apply loaded values
        SetMusicVolume(musicSlider.value);
        SetSFXVolume(sfxSlider.value);
        SetMasterVolume(MasterSlider.value);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_MUSIC, value);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_SFX, value);
    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat(PREF_MASTER, value);
    }
}
