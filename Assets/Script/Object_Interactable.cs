using UnityEngine;
using UnityEngine.Events;

public class Object_Interactable : MonoBehaviour
{
    UnityEvent<MainComponent> OnInteract = new UnityEvent<MainComponent>();

    public void On_interact_AddListerner(UnityAction<MainComponent> new_Action)
    {
        OnInteract.AddListener(new_Action);
    }

    public void Interact(MainComponent ComponnetToInteract)
    {
        OnInteract.Invoke(ComponnetToInteract);
    }
}
