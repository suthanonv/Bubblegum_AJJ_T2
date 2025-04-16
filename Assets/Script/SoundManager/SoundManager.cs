using System;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BBG_Jump,
    BBG_Noise,
    BBG_Land,
    
    BBG_toStick,
    BBG_Stick,
    BBG_toUnstick,
    BBG_Unstick,
    UI_Select,


    Rock_Move,
    Effect_Winning
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] SL;
    private static SoundManager instance = null;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        if (instance == null || instance.SL == null || (int)sound >= instance.SL.Length)
        {
            Debug.LogWarning("SoundManager: sound list not properly configured.");
            return;
        }

        SoundList soundList = instance.SL[(int)sound];
        AudioClip[] clips = soundList.sounds;

        if (clips == null || clips.Length == 0)
        {
            Debug.LogWarning($"SoundManager: No clips set for sound {sound}");
            return;
        }

        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];

        instance.audioSource.outputAudioMixerGroup = soundList.mixer;
        instance.audioSource.pitch = UnityEngine.Random.Range(soundList.minPitch, soundList.maxPitch);
        instance.audioSource.PlayOneShot(randomClip, volume * soundList.volume);
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref SL, names.Length);
        for (int i = 0; i < names.Length; i++)
        {
            SL[i].name = names[i];
        }
    }
#endif
}

[Serializable]
public struct SoundList
{
    [HideInInspector] public string name;
    [Range(0, 1)] public float volume;
    [Range(0.1f, 3f)] public float minPitch;
    [Range(0.1f, 3f)] public float maxPitch;
    public AudioMixerGroup mixer;
    public AudioClip[] sounds;
}
