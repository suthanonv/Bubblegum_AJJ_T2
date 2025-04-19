using UnityEngine;

public class Awake_Gum_Animate_control : MonoBehaviour
{
    [SerializeField] MainComponent_Transform mainComponent;
    [SerializeField] AnimationClip clip;
    Animator animator;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        SetUp();
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_PreEnter_Listener(PreEnter);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);
        spriteRenderer.enabled = false;

    }


    void PreEnter()
    {
        mainComponent.AddOnMoveListener(Walk);
        mainComponent.AddOnFinishMove(Idle);
        mainComponent.Set_GetOnMoveDuration_Func(MoveDutation);
    }

    void SetUp()
    {
        spriteRenderer.enabled = true;
    }

    void Delete()
    {

        mainComponent.RemoveOneMoveListener(Walk);
        mainComponent.RemoveOnfinishMove(Idle);
        spriteRenderer.enabled = false;
    }


    float MoveDutation()
    {
        return clip.length;
    }
    void Walk()
    {



        Vector2Int Direction = mainComponent.Current_direction;

        animator.SetFloat("X", Direction.x);
        animator.SetFloat("Y", Direction.y);

        animator.Play("Move");

        SoundManager.PlaySound(SoundType.BBG_Jump);
    }

    void Idle()
    {
        Debug.Log("Play Idle");
        animator.StopPlayback();
        animator.Play("IDLE", 0, 0f);
    }
}
