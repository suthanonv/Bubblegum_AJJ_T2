using System;
using System.Collections;
using System.Collections.Generic;
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

    Effect_EnterWinning,
    Effect_Winning
}

public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] SL;
    private static SoundManager instance = null;

    private const int PoolSizePerSound = 3;

    private class SoundSource
    {
        public AudioSource source;
        public bool inUse;
    }

    private Dictionary<SoundType, List<SoundSource>> sourcePools = new();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Create pool per sound type
            foreach (SoundType type in Enum.GetValues(typeof(SoundType)))
            {
                GameObject groupGO = new GameObject($"Pool_{type}");
                groupGO.transform.SetParent(transform);

                List<SoundSource> pool = new List<SoundSource>();
                for (int i = 0; i < PoolSizePerSound; i++)
                {
                    GameObject audioGO = new GameObject($"Audio_{type}_{i}");
                    audioGO.transform.SetParent(groupGO.transform);
                    AudioSource src = audioGO.AddComponent<AudioSource>();
                    pool.Add(new SoundSource { source = src, inUse = false });
                }

                sourcePools[type] = pool;
            }
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
            Debug.LogWarning("SoundManager: Invalid sound or uninitialized.");
            return;
        }

        SoundList soundList = instance.SL[(int)sound];
        if (soundList.sounds == null || soundList.sounds.Length == 0)
        {
            Debug.LogWarning($"SoundManager: No clips assigned to {sound}");
            return;
        }

        AudioClip clip = soundList.sounds[UnityEngine.Random.Range(0, soundList.sounds.Length)];
        List<SoundSource> pool = instance.sourcePools[sound];

        foreach (var s in pool)
        {
            if (!s.inUse)
            {
                s.inUse = true;
                AudioSource source = s.source;

                source.outputAudioMixerGroup = soundList.mixer;
                source.volume = volume * soundList.volume;
                source.pitch = UnityEngine.Random.Range(soundList.minPitch, soundList.maxPitch);
                source.clip = clip;
                source.Play();

                instance.StartCoroutine(instance.ReleaseAfter(source, s, clip.length / source.pitch));
                return;
            }
        }

        Debug.LogWarning($"All AudioSources for {sound} are busy.");
    }

    private IEnumerator ReleaseAfter(AudioSource source, SoundSource s, float delay)
    {
        yield return new WaitForSeconds(delay);
        s.inUse = false;
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
