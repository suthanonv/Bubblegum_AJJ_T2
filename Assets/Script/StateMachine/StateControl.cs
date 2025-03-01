using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[RequireComponent(typeof(MainComponent))]
public class StateControl<T> : MonoBehaviour where T : Enum
{
    [SerializeField] T _currentState;
    Dictionary<T, StateBehaviour<T>> StateToBehave = new Dictionary<T, StateBehaviour<T>>();


    private void Start()
    {
        SetUp();
        IntilizedState();
        this.GetComponent<MainComponent>().GameObject_SetFunc(GetState_Object);
    }

    void SetUp()
    {
        foreach (Transform i in this.transform)
        {
            var StateToStore = i.gameObject.GetComponent<StateBehaviour<T>>();

            if (StateToStore == null) continue;

            StateToBehave.Add(StateToStore.state, StateToStore);
        }
    }
    void IntilizedState()
    {
        StateToBehave[_currentState].OnEnterState();
    }


    public void SetState(T newstate)
    {
        T oldState = _currentState;
        StateToBehave[oldState].OnExitState();
        _currentState = newstate;
        StateToBehave[newstate].OnEnterState();
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
