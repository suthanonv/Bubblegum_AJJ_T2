using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleGumBeamController : MonoBehaviour
{
    List<Main_BubbleGumstate> AllGums = new List<Main_BubbleGumstate>();

    [SerializeField] AnimationClip _beamUpClip;
    UndoAndRedoController undocontroler;
    Input_handle _inputHandle;
    private void Start()
    {
        FindAnyObjectByType<LevelLoader>().AddListener(OnLoadNewScene);
        FindAnyObjectByType<GameSystem>().SetHandleWinTransition(PlayAllAnimation);
        undocontroler = FindAnyObjectByType<UndoAndRedoController>();
        _inputHandle = FindAnyObjectByType<Input_handle>();
        OnLoadNewScene();
    }

    public void OnLoadNewScene()
    {
        undocontroler.EnableUndo = true;
        _inputHandle.EnableMove = true;
        foreach (var i in FindObjectsByType<Main_BubbleGumstate>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            AllGums.Add(i);
        }
    }

    void PlayAllAnimation(System.Action Callback)
    {
        CurrentLevelClass.SetNewLevel(SceneManager.GetActiveScene().buildIndex - 1);

        StartCoroutine(Animated(Callback));
    }


    IEnumerator Animated(System.Action Callback)
    {
        SoundManager.PlaySound(SoundType.Effect_Warp, 1f);
        undocontroler.EnableUndo = false;
        _inputHandle.EnableMove = false;
        foreach (var gum in AllGums)
        {
            gum.SetState(Bubble_Gum_State.Normal);

        }

        yield return new WaitForSeconds(0.5f);
        FindAnyObjectByType<Wining_Check>().OnDisableWinTile_Sprite();

        foreach (var gum in AllGums)
        {
            gum.SetState(Bubble_Gum_State.Win);

        }


        yield return new WaitForSeconds(_beamUpClip == null ? 0f : _beamUpClip.length);


        Callback.Invoke();
    }
}
