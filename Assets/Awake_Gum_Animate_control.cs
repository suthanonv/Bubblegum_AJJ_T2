using UnityEngine;

public class Awake_Gum_Animate_control : MonoBehaviour
{
    [SerializeField] MainComponent mainComponent;
    [SerializeField] AnimationClip clip;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        mainComponent.Transform.Set_GetOnMoveDuration_Func(MoveDutation);
        mainComponent.Transform.AddOnMoveListener(Walk);
        mainComponent.Transform.AddOnFinishMove(Idle);
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
