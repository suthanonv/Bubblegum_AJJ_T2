using System;
using System.Collections;
using UnityEngine;

public class Visual_StickToNormal : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Stick;
    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Normal;


    [SerializeField] MainComponent_Transform MainComponent_Tranform;
    [SerializeField] AnimationClip _clip;
    [SerializeField] string AnimationState;
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    Input_handle _inputHandle;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;
        _inputHandle = FindAnyObjectByType<Input_handle>();

    }


    void InstantEndTransition(Vector2Int none)
    {
        StopAllCoroutines();
        OnEnd();
        _callback?.Invoke();


    }

    Action _callback;
    protected override void OnTransition(Action CallBack, Action PreEnter)
    {
        _callback = CallBack;
        _inputHandle.AddMovementListener(InstantEndTransition);
        PreEnter?.Invoke();
        _animator.enabled = true;
        _spriteRenderer.enabled = true;
        _spriteRenderer.sortingLayerName = GetSpriteOrder(MainComponent_Tranform.CurretionDirectionEnum);
        _animator.SetFloat("x", MainComponent_Tranform.Current_direction.x);
        _animator.SetFloat("y", MainComponent_Tranform.Current_direction.y);
        _animator.Play(AnimationState, 0, 0f);

        StartCoroutine(Transition(CallBack));
    }

    string GetSpriteOrder(Direction direct)
    {
        if (direct == Direction.Up) return "Elevated2InfrontGum";
        else return "ElevatedBehindGum";
    }



    void OnEnd()
    {
        _animator.enabled = false;
        _spriteRenderer.enabled = false;
        _inputHandle.RemoveMovementListener(InstantEndTransition);
    }

    IEnumerator Transition(Action CallBack)
    {

        yield return new WaitForSeconds(GetDuration());

        OnEnd();
        CallBack.Invoke();
    }


    float GetDuration()
    {
        return _clip.length;
    }

}
