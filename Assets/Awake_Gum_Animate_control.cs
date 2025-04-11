using UnityEngine;

public class Awake_Gum_Animate_control : MonoBehaviour
{
    [SerializeField] MainComponent_Transform mainComponent;
    [SerializeField] AnimationClip clip;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SetUp();
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);
        this.gameObject.SetActive(false);

    }

    void SetUp()
    {
        this.gameObject.SetActive(true);
        mainComponent.AddOnMoveListener(Walk);
        mainComponent.AddOnFinishMove(Idle);
        mainComponent.Set_GetOnMoveDuration_Func(MoveDutation);
    }

    void Delete()
    {
        this.gameObject.SetActive(false);

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

        animator.SetFloat("X", Direction.x);
        animator.SetFloat("Y", Direction.y);

        animator.Play("Move");
    }

    void Idle()
    {
        animator.StopPlayback();
        animator.Play("IDLE");
    }
}
