using UnityEngine;

public class Sticky_box : MonoBehaviour
{
    [SerializeField] MainComponent mainComponent;
    Attach_Moveable_List Attach_Moveable_List;

    private void Start()
    {
        Attach_Moveable_List = mainComponent.FindComponnet_InChild<Attach_Moveable_List>();
    }


    public void Sticky_Interact(MainComponent mainComponent)
    {
        if (mainComponent.TryGetComponent<StateControl<Bubble_Gum_State>>(out StateControl<Bubble_Gum_State> state))
        {
            state.SetState(Bubble_Gum_State.Stick);
            Attach_Moveable_List attach_Moveable_List = state.GetComponent<MainComponent>().FindComponnet_InChild<Attach_Moveable_List>();

            attach_Moveable_List.Add_New_Moveable(Attach_Moveable_List.Get_List());
            Attach_Moveable_List.Add_New_Moveable(attach_Moveable_List.Get_List());

        }
    }
}
