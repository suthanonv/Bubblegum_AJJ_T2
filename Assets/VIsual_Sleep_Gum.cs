using UnityEngine;

public class VIsual_Sleep_Gum : MonoBehaviour
{
    string State = "Base Layer.Sleep.Sleep_Idle";
    [SerializeField] Animator animator;
    public void EnterState()
    {
        animator.Play(State, 0, 0);
    }

    public void ExiteState()
    {

    }
}
