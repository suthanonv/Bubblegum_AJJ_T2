using System;
using UnityEngine;
public class MainComponent : MonoBehaviour
{



    private void Awake()
    {
        GetGameObject = () => this.gameObject;
    }



    Func<MainComponent_Transform> _transform = () => null;
    public MainComponent_Transform Transform => _transform?.Invoke();

    Func<GameObject> GetGameObject = () => null;
    public GameObject Main_GameObject => GetGameObject?.Invoke();

    public void SetTransform(Func<MainComponent_Transform> Set_Transform)
    {
        _transform = Set_Transform;
    }



    public void GameObject_SetFunc(Func<GameObject> newFunc)
    {
        GetGameObject = newFunc;
    }


    #region Find Component
    public bool TryFindComponent_InChild<A>(out A Component) where A : class
    {
        Component = null;

        Component = Main_GameObject.transform.GetComponentInChildren<A>();


        return Component != null;
    }

    public B FindComponnet_InChild<B>() where B : class
    {
        B _Component = null;


        _Component = Main_GameObject.transform.GetComponentInChildren<B>();



        return _Component;


    }
    #endregion
}
