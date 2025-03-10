using UnityEngine;
using UnityEngine.Events;

public abstract class Grid_Collider : MonoBehaviour
{


    public UnityAction<MainComponent> OnEnter => _OnEnter;
    public UnityAction<MainComponent> OnExit => _OnExit;
    protected abstract void _OnEnter(MainComponent main);
    protected abstract void _OnExit(MainComponent main);

}
