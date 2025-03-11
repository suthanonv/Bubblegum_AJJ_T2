using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(All_Sticky_Gum_Holder))]
public class Un_Stick : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<All_Sticky_Gum_Holder>().Add_UnStick_Listener(Unstick);
    }


    public void Unstick(List<StateControl<Bubble_Gum_State>> All_Sticky_Gum)
    {
        List<StateControl<Bubble_Gum_State>> copyList = new List<StateControl<Bubble_Gum_State>>(All_Sticky_Gum);

        foreach (StateControl<Bubble_Gum_State> i in copyList)
        {
            {
                i.SetState(Bubble_Gum_State.Normal);
            }
        }
    }
}
