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
    [SerializeField] float VideoTime;

#if UNITY_EDITOR
    [SerializeField] VideoClip _videoClip;
    private void OnValidate()
    {

        if (_videoClip == null) return;
        VideoTime = (float)_videoClip.length;
    }
#endif
    private void BeginMethod()
    {
        OnStart?.Invoke();

        var escMenu = FindAnyObjectByType<EscMenuManager>(FindObjectsInactive.Include);
        if (escMenu != null) escMenu.enabled = false;

        var inputHandle = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        if (inputHandle != null) inputHandle.enabled = false;

        var bgm = FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include);
        if (bgm != null) bgm.enabled = false;
    }




    private void StopMethod()
    {
        OnStop?.Invoke();

        if (video.isPlaying)
        {
            video.Stop();
        }

        Invoke("Delayed", 0.0001f);

    }

    void Delayed()
    {
        var escMenu = FindAnyObjectByType<EscMenuManager>(FindObjectsInactive.Include);
        if (escMenu != null) escMenu.enabled = true;

        var inputHandle = FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include);
        if (inputHandle != null) inputHandle.enabled = true;

        var bgm = FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include);
        if (bgm != null) bgm.enabled = true;
    }


    private void Awake()
    {

        if (playedCinematicList.Contains(Cinimatic_index))
        {
            Debug.Log($"[Cinematic_manager] Cinematic {Cinimatic_index} already played. Skipping.");
            return;
        }


        playedCinematicList.Add(Cinimatic_index);
        FindAnyObjectByType<ButtonCommand_Handle>()?.Add_Esc_Action(StopMethod);
        video = GetComponent<VideoPlayer>();
        _clip = video.clip;

        WebglVideoPlayer webPlay = GetComponent<WebglVideoPlayer>();
        if (webPlay == null)
        {
            video.Play();
        }
        else
        {
        }
        StartCoroutine(VideoClip(VideoTime));
    }

    IEnumerator VideoClip(double Time)
    {
        BeginMethod();
        yield return new WaitForSeconds((float)Time - RemoveBackScene);
        StopMethod();
    }

}
