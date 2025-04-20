using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] int Cinimatic_index = 0;

    static List<int> playedCinematicList = new List<int>();


    private void BeginMethod()
    {
        FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include).enabled = false;
        FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include).enabled = false;
    }

    private void StopMethod()
    {
        FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include).enabled = true;
        FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include).enabled = true;

    }




    private void Awake()
    {
        OnStart.AddListener(BeginMethod);
        OnStop.AddListener(StopMethod);

        if (playedCinematicList.Contains(Cinimatic_index)) return;

        playedCinematicList.Add(Cinimatic_index);

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
