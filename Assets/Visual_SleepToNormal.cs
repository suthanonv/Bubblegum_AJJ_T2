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

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator.enabled = false;
        _spriteRenderer.enabled = false;
    }

    protected override void OnTransition(Action CallBack)
    {
        _animator.enabled = true;
        _spriteRenderer.enabled = true;
        _animator.StopPlayback();
        _animator.Play(_animationStateName, 0, 0f);
        StartCoroutine(Transition(CallBack));
    }

    IEnumerator Transition(System.Action CallBack)
    {
        yield return new WaitForSeconds(GetDuration());

        _animator.enabled = false;
        _spriteRenderer.enabled = false;
        CallBack.Invoke();
    }


    float GetDuration()
    {
        return _clip.length;
    }
}
