using System;
using UnityEngine;

public class StateTransition<T> : MonoBehaviour where T : Enum
{

    protected virtual T CurrentState { get; }
    protected virtual T _nextState { get; }
    System.Action<Action, Action> _action;


    protected virtual void Awake()
    {
        _action += OnTransition;
    }

    protected virtual void OnTransition(System.Action CallBack, System.Action PreEnter)
    {
        CallBack.Invoke();
    }


    public bool IsTransitionExit(T currentState, T nextState)
    {
        return (currentState.Equals(CurrentState) && nextState.Equals(_nextState));
    }

    public void GetTransition(T currentState, T nextState, System.Action CallBack, System.Action PreEnter)
    {
        if (currentState.Equals(CurrentState) && nextState.Equals(_nextState))
        {
            _action?.Invoke(CallBack, PreEnter);
        }
        return;
    }


}
