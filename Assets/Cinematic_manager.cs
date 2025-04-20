using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

public class Cinematic_manager : MonoBehaviour
{
    VideoPlayer video;
    VideoClip _clip;

    [SerializeField] UnityEvent OnStart = new UnityEvent();
    [SerializeField] UnityEvent OnStop = new UnityEvent();
    [SerializeField] float RemoveBackScene = 1;

    static bool hasbeenPlayed = false;

    private void Awake()
    {

        if (hasbeenPlayed) return;
        hasbeenPlayed = true;

        video = GetComponent<VideoPlayer>();
        _clip = video.clip;
        video.Play();
        StartCoroutine(VideoClip(_clip.length));

    }

    IEnumerator VideoClip(double Time)
    {
        OnStart?.Invoke();
        yield return new WaitForSeconds((float)Time - RemoveBackScene);
        OnStop?.Invoke();

    }

}
