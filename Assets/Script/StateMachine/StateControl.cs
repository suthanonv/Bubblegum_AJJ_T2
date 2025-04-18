using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MainComponent))]
public class StateControl<T> : MonoBehaviour where T : Enum
{
    public T GetCurrentState() => _currentState;
    [SerializeField] protected T _currentState;
    Dictionary<T, StateBehaviour<T>> StateToBehave = new Dictionary<T, StateBehaviour<T>>();

    private void Awake()
    {
        SetUp();
        this.GetComponent<MainComponent>().GameObject_SetFunc(GetState_Object);
        IntilizedState();
    }

    List<StateTransition<T>> StateTransitionList = new List<StateTransition<T>>();

    void SetUp()
    {
        foreach (Transform i in this.transform)
        {
            var StateToStore = i.gameObject.GetComponent<StateBehaviour<T>>();

            if (StateToStore != null)
            {

                StateToBehave.Add(StateToStore.state, StateToStore);
            }

            var StateTranstion = i.gameObject.GetComponent<StateTransition<T>>();

            if (StateTranstion != null)
            {
                StateTransitionList.Add(StateTranstion);
            }

        }
    }
    void IntilizedState()
    {
        StateToBehave[_currentState].PreEnter();
        StateToBehave[_currentState].OnEnterState();
    }

    T _preNewState;


    public virtual void SetState(T newstate)
    {
        T oldState = _currentState;
        StateToBehave[oldState].OnExitState();
        _preNewState = newstate;
        _currentState = _preNewState;
        foreach (var i in StateTransitionList)
        {
            if (i.IsTransitionExit(oldState, newstate) == true)
            {

                i.GetTransition(oldState, newstate, SetNewState, StateToBehave[_preNewState].PreEnter);
                return;
            }
        }
        StateToBehave[_preNewState].PreEnter();
        SetNewState();


    }


    void SetNewState()
    {
        StateToBehave[_preNewState].OnEnterState();
    }

    public GameObject Get_Specific_State_Object(T state)
    {
        return StateToBehave[state].gameObject;
    }

    public GameObject GetState_Object()
    {
        return StateToBehave[_currentState].gameObject;
    }




}
