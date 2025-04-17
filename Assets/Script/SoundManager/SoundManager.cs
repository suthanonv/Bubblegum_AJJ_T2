using System;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    BBG_Jump,
    BBG_GrassNoise,
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
            return;

        SoundList soundList = instance.SL[(int)sound];
        if (soundList.sounds == null || soundList.sounds.Length == 0)
            return;

        AudioClip randomClip = soundList.sounds[UnityEngine.Random.Range(0, soundList.sounds.Length)];

        
        GameObject tempGO = new GameObject("TempAudio");
        AudioSource aSource = tempGO.AddComponent<AudioSource>();
        aSource.outputAudioMixerGroup = soundList.mixer;
        aSource.clip = randomClip;
        aSource.volume = volume * soundList.volume;
        aSource.pitch = UnityEngine.Random.Range(soundList.minPitch, soundList.maxPitch);
        aSource.Play();

        UnityEngine.Object.Destroy(tempGO, randomClip.length / aSource.pitch);
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
