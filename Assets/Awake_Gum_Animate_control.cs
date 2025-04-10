using UnityEngine;

public class Awake_Gum_Animate_control : MonoBehaviour
{
    [SerializeField] MainComponent mainComponent;
    [SerializeField] AnimationClip clip;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SetUp();
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);

    }

    void SetUp()
    {
        this.gameObject.SetActive(true);
        mainComponent.Transform.AddOnMoveListener(Walk);
        mainComponent.Transform.AddOnFinishMove(Idle);
        mainComponent.Transform.Set_GetOnMoveDuration_Func(MoveDutation);
    }

    void Delete()
    {
        this.gameObject.SetActive(false);

        mainComponent.Transform.RemoveOneMoveListener(Walk);
        mainComponent.Transform.RemoveOnfinishMove(Idle);

    }


    float MoveDutation()
    {
        return clip.length;
    }
    void Walk()
    {


        Vector2Int Direction = mainComponent.Transform.Current_direction;

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
