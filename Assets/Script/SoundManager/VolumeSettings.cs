using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    const string MIXER_MUSIC = "MusicVolumn";
    const string MIXER_SFX = "SFXVolumn";


    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        musicSlider.onValueChanged.AddListener(SetSFXVolume);
    }
    private void SetMusicVolume(float value)
    {
        audioMixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }
    private void SetSFXVolume(float value)
    {
        audioMixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}
