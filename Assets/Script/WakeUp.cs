using UnityEngine;

public class WakeUp : MonoBehaviour
{
    [SerializeField] Main_BubbleGumstate gumState_holder;
    public void GumWokeUp()
    {
        Debug.Log("Gum Sleep => Gum Woke up ");
        gumState_holder.SetState(Bubble_Gum_State.Normal);
    }
}
