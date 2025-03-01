using UnityEngine;

public class WakeUp : MonoBehaviour
{
    [SerializeField] Main_BubbleGumstate gumState_holder;
    public void GumWokeUp()
    {
        gumState_holder.SetState(Bubble_Gum_State.Normle);
    }
}
