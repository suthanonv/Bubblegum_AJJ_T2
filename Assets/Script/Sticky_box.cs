using System.Collections.Generic;
using UnityEngine;

public class Sticky_box : MonoBehaviour
{
    [SerializeField] MainComponent mainComponent;
    Attach_Moveable_List Attach_Moveable_List;

    [SerializeField] List<MainComponent> Starting_Gum;

    private void Start()
    {
        Attach_Moveable_List = mainComponent.FindComponnet_InChild<Attach_Moveable_List>();

        foreach (var item in Starting_Gum)
        {
            SetUpGum(item);
        }
    }


    public void Sticky_Interact(MainComponent mainComponent)
    {
        SetUpGum(mainComponent);
    }


    void SetUpGum(MainComponent mainComponent)
    {
        if (mainComponent.TryGetComponent<StateControl<Bubble_Gum_State>>(out StateControl<Bubble_Gum_State> state))
        {
            state.SetState(Bubble_Gum_State.Stick);
            Attach_Moveable_List attach_Moveable_List = state.GetComponent<MainComponent>().FindComponnet_InChild<Attach_Moveable_List>();

            attach_Moveable_List.Add_New_Moveable(Attach_Moveable_List.Get_List());
            Attach_Moveable_List.Add_New_Moveable(attach_Moveable_List.Get_List());
        }

        else
        {
            if (mainComponent.TryFindComponent_InChild<Attach_Moveable_List>(out Attach_Moveable_List attach))
            {

                attach.Add_New_Moveable(Attach_Moveable_List.Get_List());
                attach.Add_New_Moveable(attach.Get_List());
            }
        }
    }
}
