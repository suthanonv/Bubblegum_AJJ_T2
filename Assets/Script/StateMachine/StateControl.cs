using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StateControl<T> : MonoBehaviour where T : Enum
{
    [SerializeField] T _currentState;
    Dictionary<T, StateBehaviour<T>> StateToBehave = new Dictionary<T, StateBehaviour<T>>();


    private void Start()
    {
        SetUp();
        IntilizedState();
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


    public GameObject GetState_Object()
    {
        return StateToBehave[_currentState].gameObject;
    }

    public bool TryFindComponent_InChild<A>(GameObject obj, out A Component) where A : class
    {
        Component = null;

        foreach (Transform i in obj.transform)
        {
            if (i.TryGetComponent<A>(out A _component))
            {
                Component = _component;
                return true;
            }
        }

        return false;
    }


}
