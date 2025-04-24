using System;
using System.Collections;
using UnityEngine;
public class Visual_NormalToStick : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Normal;
    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Stick;


    [SerializeField] MainComponent_Transform MainComponent_Tranform;
    [SerializeField] AnimationClip _clip;
    string AnimationState = "Base Layer.Normal To Stick";
    [SerializeField] Animator _Animator;
    SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _spriteRenderer = _Animator.GetComponent<SpriteRenderer>();
    }

    protected override void OnTransition(Action CallBack, Action PreEnter)
    {
        PreEnter?.Invoke();
        MainComponent_Tranform.FreezeRotation = true;

        _spriteRenderer.sortingLayerName = GetSpriteOrder(MainComponent_Tranform.CurretionDirectionEnum);


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


    IEnumerator Transition(Action CallBack)
    {

        yield return new WaitForSeconds(GetDuration());


        CallBack.Invoke();
    }


    float GetDuration()
    {
        return _clip.length;
    }

}
