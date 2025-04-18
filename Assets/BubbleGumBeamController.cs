using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGumBeamController : MonoBehaviour
{
    List<Main_BubbleGumstate> AllGums = new List<Main_BubbleGumstate>();

    [SerializeField] AnimationClip _beamUpClip;
    UndoAndRedoController undocontroler;
    private void Start()
    {
        FindAnyObjectByType<LevelLoader>().AddListener(SetUp);
        FindAnyObjectByType<GameSystem>().SetHandleWinTransition(PlayAllAnimation);
        undocontroler = FindAnyObjectByType<UndoAndRedoController>();
        SetUp();
    }

    void SetUp()
    {
        undocontroler.EnableUndo = true;
        foreach (var i in FindObjectsByType<Main_BubbleGumstate>(FindObjectsSortMode.None))
        {
            AllGums.Add(i);
        }
    }

    void PlayAllAnimation(System.Action Callback)
    {
        StartCoroutine(Animated(Callback));
    }


    IEnumerator Animated(System.Action Callback)
    {
        undocontroler.EnableUndo = false;
        foreach (var gum in AllGums)
        {
            gum.SetState(Bubble_Gum_State.Normal);

        }

        yield return new WaitForSeconds(0.5);

        foreach (var gum in AllGums)
        {
            gum.SetState(Bubble_Gum_State.Win);

        }

        yield return new WaitForSeconds(_beamUpClip == null ? 0f : _beamUpClip.length);


        Callback.Invoke();
    }
}
