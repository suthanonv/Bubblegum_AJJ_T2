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
        OnStart?.Invoke();
        FindAnyObjectByType<EscMenuManager>(FindObjectsInactive.Include).enabled = false;
        FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include).enabled = false;
        FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include).enabled = false;
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
        FindAnyObjectByType<EscMenuManager>(FindObjectsInactive.Include).enabled = true;

        FindAnyObjectByType<Input_handle>(FindObjectsInactive.Include).enabled = true;
        FindAnyObjectByType<BackGroundMusic>(FindObjectsInactive.Include).enabled = true;
    }


    private void Awake()
    {

        FindAnyObjectByType<ButtonCommand_Handle>().Add_Esc_Action(StopMethod);


        if (playedCinematicList.Contains(Cinimatic_index)) return;

        playedCinematicList.Add(Cinimatic_index);

        video = GetComponent<VideoPlayer>();
        _clip = video.clip;
        video.Play();
        StartCoroutine(VideoClip(_clip.length));

    }

    IEnumerator VideoClip(double Time)
    {
        BeginMethod();
        yield return new WaitForSeconds((float)Time - RemoveBackScene);
        StopMethod();
    }

}
