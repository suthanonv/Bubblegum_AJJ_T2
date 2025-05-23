using System;
using UnityEngine;
using UnityEngine.Events;


public abstract class StateBehaviour<T> : MonoBehaviour where T : Enum
{
    public abstract T state { get; }

    [SerializeField] UnityEvent On_EnterState = new UnityEvent();
    [SerializeField] UnityEvent On_ExitState = new UnityEvent();
    [SerializeField] UnityEvent On_PreEnterState = new UnityEvent();

    public virtual void PreEnter()
    {
        On_PreEnterState?.Invoke();
    }

    public virtual void OnEnterState()
    {
        On_EnterState?.Invoke();
    }



    public virtual void OnExitState()
    {
        On_ExitState?.Invoke();
    }

    public void Add_PreEnter_Listener(UnityAction new_action)
    {
        On_PreEnterState.AddListener(new_action);
    }

    public void Add_EnterState_Listner(UnityAction new_Action)
    {
        On_EnterState.AddListener(new_Action);
    }

    public void Add_ExitState_Listener(UnityAction new_Action)
    {
        On_ExitState.AddListener(new_Action);
    }

}


