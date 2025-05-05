using UnityEngine;

public class Awake_Gum_Animate_control : MonoBehaviour
{
    [SerializeField] MainComponent_Transform mainComponent;
    [SerializeField] AnimationClip clip;
    [SerializeField] Animator Animator;
    SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = Animator.GetComponent<SpriteRenderer>();
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_PreEnter_Listener(PreEnter);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);
    }


    void PreEnter()
    {
        mainComponent.AddOnMoveListener(Walk);
        mainComponent.AddOnFinishMove(Idle);
        mainComponent.Set_GetOnMoveDuration_Func(MoveDutation);
    }

    void SetUp()
    {
        _spriteRenderer.sortingLayerName = "PlayableGameObjectvv";

    }

    void Delete()
    {
        mainComponent.RemoveOneMoveListener(Walk);
        mainComponent.RemoveOnfinishMove(Idle);
    }


    float MoveDutation()
    {
        return clip.length;
    }
    void Walk()
    {



        Vector2Int Direction = mainComponent.Current_direction;

        Animator.SetFloat("X", Direction.x);
        Animator.SetFloat("Y", Direction.y);

        Animator.Play("Base Layer.Normal.Move", 0, 0f);

        SoundManager.PlaySound(SoundType.BBG_Jump);
    }

    void Idle()
    {
    }
}
