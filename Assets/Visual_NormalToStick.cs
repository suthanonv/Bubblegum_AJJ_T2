using System;
using System.Collections;
using UnityEngine;
public class Visual_NormalToStick : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Normal;
    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Stick;


    [SerializeField] MainComponent_Transform MainComponent_Tranform;
    [SerializeField] AnimationClip _clip;
    [SerializeField] string AnimationState;
    Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    protected override void OnTransition(Action CallBack)
    {
        _animator.enabled = true;

        _animator.StopPlayback();

        _animator.SetFloat("x", MainComponent_Tranform.Current_direction.x);
        _animator.SetFloat("y", MainComponent_Tranform.Current_direction.y);
        _animator.Play(AnimationState);

        StartCoroutine(Transition(CallBack));
    }


    IEnumerator Transition(Action CallBack)
    {

        yield return new WaitForSeconds(GetDuration());

        _animator.enabled = false;
        CallBack.Invoke();
    }


    float GetDuration()
    {
        return _clip.length;
    }

}
