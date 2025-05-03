using System;
using System.Collections;
using UnityEngine;

public class Visual_StickToNormal : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Stick;
    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Normal;


    [SerializeField] MainComponent_Transform MainComponent_Tranform;
    [SerializeField] AnimationClip _clip;
    string AnimationState = "Base Layer.Stick To Normal";
    [SerializeField] Animator _Animator;
    Input_handle _inputHandle;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = _Animator.GetComponent<SpriteRenderer>();
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

        spriteRenderer.sortingLayerName = GetSpriteOrder(MainComponent_Tranform.CurretionDirectionEnum);
        _Animator.SetFloat("X", MainComponent_Tranform.Current_direction.x);
        _Animator.SetFloat("Y", MainComponent_Tranform.Current_direction.y);
        _Animator.Play(AnimationState, 0, 0f);

        StartCoroutine(Transition(CallBack));
    }

    string GetSpriteOrder(Direction direct)
    {
        if (direct == Direction.Up) return "Elevated2InfrontGum";
        else return "ElevatedBehindGum";
    }



    void OnEnd()
    {

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
