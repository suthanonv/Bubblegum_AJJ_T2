using System;
using System.Collections;
using UnityEngine;
public class Visual_SleepToNormal : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Sleep;

    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Normal;

    [SerializeField] AnimationClip _clip;

    Animator _animator;
    [SerializeField] string _animationStateName;
    SpriteRenderer _spriteRenderer;
    Input_handle _inputHandle;
    private void Start()
    {
        _inputHandle = FindAnyObjectByType<Input_handle>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.enabled = false;
        _spriteRenderer.enabled = false;
    }


    Action _callback;

    protected override void OnTransition(Action CallBack, Action PreEnter)
    {

        _inputHandle.AddMovementListener(InstantEndTransition);
        _callback = CallBack;
        Debug.Log("being played");
        PreEnter?.Invoke();
        _animator.enabled = true;
        _spriteRenderer.enabled = true;
        _animator.StopPlayback();
        _animator.Play(_animationStateName, 0, 0f);
        StartCoroutine(Transition(CallBack));
    }

    void InstantEndTransition(Vector2Int none)
    {
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
        _animator.enabled = false;
        _spriteRenderer.enabled = false;
        _inputHandle.RemoveMovementListener(InstantEndTransition);
    }

    float GetDuration()
    {
        return _clip.length;
    }
}
