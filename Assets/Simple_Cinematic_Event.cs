using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Video;
public class Simple_Cinematic_Event : MonoBehaviour
{
    VideoPlayer _vd_player;
    [SerializeField] SceneAsset NextScene;
    int _nextScene;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (NextScene == null) return;

        _nextScene = Level_Progress_Manager.GetBuildIndexByName(NextScene.name);
    }
#endif

    void Start()
    {

        _vd_player = GetComponent<VideoPlayer>();

        _vd_player.Play();

        StartCoroutine(VideoTimeCountDown(_vd_player.clip.length));
    }


    IEnumerator VideoTimeCountDown(double time)
    {
        yield return new WaitForSeconds((float)time);

        LoadNextLevel();
    }


    void LoadNextLevel()
    {
        FindAnyObjectByType<LevelLoader>().LoadLevel(_nextScene);
    }
}
