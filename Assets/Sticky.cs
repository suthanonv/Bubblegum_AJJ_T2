using UnityEngine;

public class Sticky : MonoBehaviour
{
    Object_Interactable Object_Interactable;




    public void Sticky_Interact(MainComponent mainComponent)
    {
        if (mainComponent.TryGetComponent<StateControl<Bubble_Gum_State>>(out StateControl<Bubble_Gum_State> state))
        {
            state.SetState(Bubble_Gum_State.Stick);
            Attach_Moveable_List attach_Moveable_List = state.GetComponent<MainComponent>().FindComponnet_InChild<Attach_Moveable_List>();

            attach_Moveable_List.Remove_Moveable(attach_Moveable_List);

        }
    }
}
