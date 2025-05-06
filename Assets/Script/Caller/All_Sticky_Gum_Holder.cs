using System;
using System.Collections.Generic;
using UnityEngine;
public class All_Sticky_Gum_Holder : MonoBehaviour
{
    System.Action<List<StateControl<Bubble_Gum_State>>> unstick;
    List<StateControl<Bubble_Gum_State>> StickedGum = new List<StateControl<Bubble_Gum_State>>();



    private void Start()
    {
        FindAnyObjectByType<ButtonCommand_Handle>().Add_Space_Action(UnStick_Handle);
    }


    Action OnBubbleGumStick;
    public void AddOnBubbleGumStickEvent(Action Event)
    {
        OnBubbleGumStick += Event;
    }

    public void Add_Sticky_Gum(StateControl<Bubble_Gum_State> gum)
    {
        StickedGum.Add(gum);
        OnBubbleGumStick?.Invoke();
    }

    public void Remove_Sticky_Gum(StateControl<Bubble_Gum_State> gum)
    {
        StickedGum.Remove(gum);
    }

    public void UnStick_Handle()
    {
        unstick.Invoke(StickedGum);
        OnUnStickGum?.Invoke();
    }


    public void Add_UnStick_Listener(System.Action<List<StateControl<Bubble_Gum_State>>> newFunc)
    {
        unstick += newFunc;
    }


    Action OnUnStickGum;

    public void Add_OnUnStickGum(Action newAct)
    {
        OnUnStickGum += newAct;
    }


}
