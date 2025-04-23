using UnityEngine;

public class Action<T> : MonoBehaviour
{
    System.Action<T> action;


    public void AddListener(System.Action<T> new_action)
    {
        this.action += new_action;
    }

    public void Calling(T _Para)
    {
        action?.Invoke(_Para);
    }


}
