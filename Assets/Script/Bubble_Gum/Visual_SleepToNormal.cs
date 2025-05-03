using System;
using System.Collections;
using UnityEngine;
public class Visual_SleepToNormal : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Sleep;

    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Normal;

    [SerializeField] AnimationClip _clip;
    [SerializeField] Animator Animator;

    string _animationStateName = "Base Layer.Sleep To Normal";
    Input_handle _inputHandle;
    private void Start()
    {
        _inputHandle = FindAnyObjectByType<Input_handle>();
    }


    Action _callback;

    protected override void OnTransition(Action CallBack, Action PreEnter)
    {
        _inputHandle.AddMovementListener(InstantEndTransition);
        _callback = CallBack;
        Debug.Log("being played");
        PreEnter?.Invoke();

        Animator.Play(_animationStateName, 0, 0f);
        StartCoroutine(Transition(CallBack));
    }

    void InstantEndTransition(Vector2Int none)
    {
        Debug.Log("Instant Ended");
        StopAllCoroutines();
        OnEnd();
        _callback?.Invoke();


    }




    IEnumerator Transition(System.Action CallBack)
    {
        yield return new WaitForSeconds(GetDuration());

        OnEnd();
        CallBack.Invoke();
    }


    void OnEnd()
    {
        _inputHandle.RemoveMovementListener(InstantEndTransition);
    }

    float GetDuration()
    {
        return _clip.length;
    }
}
