using System;

public struct StateTransition<T> where T : Enum
{

    private T _currentState;
    private T _nextState;
    public Action<Action> Action;

    public StateTransition(T currentState, T nextState, Action<Action> action)
    {
        _currentState = currentState;
        _nextState = nextState;
        Action = action;
    }

    public Action<Action> GetTransition(T currentState, T nextState)
    {
        if (currentState.Equals(_currentState) && nextState.Equals(_nextState))
        {
            return Action;
        }
        return null;
    }


}
