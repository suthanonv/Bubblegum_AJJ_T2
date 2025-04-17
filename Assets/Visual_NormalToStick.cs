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
    SpriteRenderer _spriteRenderer;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

    }

    protected override void OnTransition(Action CallBack)
    {
        _animator.enabled = true;
        _spriteRenderer.enabled = true;
        _spriteRenderer.sortingOrder = GetSpriteOrder(MainComponent_Tranform.CurretionDirectionEnum);
        _animator.StopPlayback();

        _animator.SetFloat("x", MainComponent_Tranform.Current_direction.x);
        _animator.SetFloat("y", MainComponent_Tranform.Current_direction.y);
        _animator.Play(AnimationState , 0 , 0f);

        StartCoroutine(Transition(CallBack));
    }


    int GetSpriteOrder(Direction direct)
    {
        if (direct == Direction.Up) return Tile_SpriteOrder.GetSpriteOrder(OBjectType.StickAble) + 1;
        else return Tile_SpriteOrder.GetSpriteOrder(OBjectType.StickAble) - 1;
    }


    IEnumerator Transition(Action CallBack)
    {

        yield return new WaitForSeconds(GetDuration());

        _spriteRenderer.enabled = false;
        _animator.enabled = false;
        CallBack.Invoke();
    }


    float GetDuration()
    {
        return _clip.length;
    }

}
