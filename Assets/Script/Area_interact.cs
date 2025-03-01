using UnityEngine;
using UnityEngine.Events;

public class Area_interact : MonoBehaviour
{
    [SerializeField] UnityEvent interacted_event = new UnityEvent();


    public void Interact()
    {
        interacted_event?.Invoke();
    }
}
