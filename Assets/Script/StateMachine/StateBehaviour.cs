using System;
using UnityEngine;


public abstract class StateBehaviour<T> : MonoBehaviour where T : Enum
{
    public abstract T state { get; }


    public abstract void OnEnterState();

    public abstract void OnExitState();

}


