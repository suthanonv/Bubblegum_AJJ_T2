public class Sticky : Object_Interactable
{
    Object_Interactable Object_Interactable;

    private void Start()
    {
        Object_Interactable = GetComponent<Object_Interactable>();

        Object_Interactable.On_interact_AddListerner(Sticky_Interact);
    }

    public void Sticky_Interact(MainComponent mainComponent)
    {
        if (mainComponent.TryGetComponent<StateControl<Bubble_Gum_State>>(out StateControl<Bubble_Gum_State> state))
        {
            state.SetState(Bubble_Gum_State.Stick);
        }
    }
}
