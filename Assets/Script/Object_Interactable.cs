using UnityEngine;
using UnityEngine.Events;

public class Object_Interactable : MonoBehaviour
{
    [SerializeField] UnityEvent<MainComponent> OnInteract;

    public void Interact(MainComponent ComponnetToInteract)
    {
        OnInteract.Invoke(ComponnetToInteract);
    }
}
