using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Simple_Cinematic_Event : MonoBehaviour
{
    private VideoPlayer _vd_player;

    [Header("Editor-Only (For Inspector Dragging)")]
#if UNITY_EDITOR
    [SerializeField] private UnityEditor.SceneAsset NextSceneAsset;
#endif

    [Header("Runtime Scene Name (Auto-filled in editor)")]
    [SerializeField] private string NextSceneName;

    private int _nextSceneIndex;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (NextSceneAsset != null)
        {
            NextSceneName = NextSceneAsset.name;
        }
    }
#endif

    private void Start()
    {
        _vd_player = GetComponent<VideoPlayer>();
        _vd_player.Play();

        
        _nextSceneIndex = Level_Progress_Manager.GetBuildIndexByName(NextSceneName);

        StartCoroutine(VideoTimeCountDown(_vd_player.clip.length));
    }

    private IEnumerator VideoTimeCountDown(double time)
    {
        yield return new WaitForSeconds((float)time);
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        FindAnyObjectByType<LevelLoader>()?.LoadLevel(_nextSceneIndex);
    }
}
