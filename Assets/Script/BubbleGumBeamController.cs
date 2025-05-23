using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BubbleGumBeamController : Singleton<BubbleGumBeamController>, I_SceneChange
{
    List<Main_BubbleGumstate> AllGums = new List<Main_BubbleGumstate>();

    [SerializeField] AnimationClip _beamUpClip;
    Input_handle _inputHandle;
    private void Start()
    {
        FindAnyObjectByType<LevelLoader>().AddSceneChangeEvent(this);
        FindAnyObjectByType<GameSystem>().SetHandleWinTransition(PlayAllAnimation);
        _inputHandle = Input_handle.Instance;
    }
    #region I_SceneChange  Method
    public void OnStartScene()
    {
        UndoAndRedoController.Instance.EnableUndo = true;
        Input_handle.Instance.EnableMove = true;
        foreach (var i in FindObjectsByType<Main_BubbleGumstate>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            AllGums.Add(i);
        }
    }

    public void OnEndScene()
    {
        AllGums = new List<Main_BubbleGumstate>();
    }
    #endregion
    void PlayAllAnimation(System.Action Callback)
    {
        CurrentLevelClass.SetNewLevel(SceneManager.GetActiveScene().buildIndex - 1);

        StartCoroutine(Animated(Callback));
    }


    IEnumerator Animated(System.Action Callback)
    {
        SoundManager.PlaySound(SoundType.Effect_Warp, 1f);
        UndoAndRedoController.Instance.EnableUndo = false;
        Input_handle.Instance.EnableMove = false;
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
