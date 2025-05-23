using System.Collections.Generic;
using UnityEngine;

public class StickableBox : MonoBehaviour
{
    [SerializeField] MainComponent mainComponent;
    Grouping Attach_Moveable_List;

    [SerializeField] List<MainComponent> Starting_Gum;

    private void Start()
    {
        Attach_Moveable_List = mainComponent.FindComponnet_InChild<Grouping>();

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
        Debug.Log("Stick now");
        if (mainComponent.TryGetComponent<StateControl<Bubble_Gum_State>>(out StateControl<Bubble_Gum_State> state))
        {
            state.SetState(Bubble_Gum_State.Stick);
            Grouping attach_Moveable_List = state.GetComponent<MainComponent>().FindComponnet_InChild<Grouping>();

            attach_Moveable_List.Reset_List();

            attach_Moveable_List.AddNewObjectIntoGroup(Attach_Moveable_List.GetGroup());
            Attach_Moveable_List.AddNewObjectIntoGroup(attach_Moveable_List.GetGroup());
        }

        else
        {
            if (mainComponent.TryFindComponent_InChild<Grouping>(out Grouping attach))
            {

                attach.AddNewObjectIntoGroup(Attach_Moveable_List.GetGroup());
                Attach_Moveable_List.AddNewObjectIntoGroup(attach.GetGroup());
            }
        }
    }
}
