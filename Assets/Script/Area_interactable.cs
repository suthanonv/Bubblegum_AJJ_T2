using UnityEngine;
using UnityEngine.Events;

public class Area_interactable : MonoBehaviour
{
    [SerializeField] UnityEvent interacted_event = new UnityEvent();


    public void Interact()
    {
        Debug.Log("Begin Interact");
        interacted_event?.Invoke();
    }
}
