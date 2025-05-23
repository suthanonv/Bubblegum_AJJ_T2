using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
public class Simple_Cinematic_Event : MonoBehaviour
{
    VideoPlayer _vd_player;
    [SerializeField] float VideoTime;
    int _nextScene;
#if UNITY_EDITOR
    [SerializeField] SceneAsset NextScene;
    [SerializeField] VideoClip _videoClip;
    private void OnValidate()
    {
        if (NextScene == null) return;
        VideoTime = (float)_videoClip.length;
        _nextScene = Level_Progress_Manager.GetBuildIndexByName(NextScene.name);
    }
#endif


    private void Awake()
    {
        _vd_player = GetComponent<VideoPlayer>();
        if (_vd_player == null) LoadNextLevel();
    }

    void Start()
    {

        _vd_player = GetComponent<VideoPlayer>();
        WebglVideoPlayer webGlplay = GetComponent<WebglVideoPlayer>();
        if (webGlplay)
        {
        }
        else
        {
            _vd_player.Play();
        }
        StartCoroutine(VideoTimeCountDown(VideoTime));




    }


    IEnumerator VideoTimeCountDown(float time)
    {
        yield return new WaitForSeconds((float)time);

        LoadNextLevel();
    }


    void LoadNextLevel()
    {
        FindAnyObjectByType<LevelLoader>().LoadLevel(_nextScene);
    }
}
