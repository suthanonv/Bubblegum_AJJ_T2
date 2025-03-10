using System;
using UnityEngine;
public class MainComponent : MonoBehaviour
{

    Grid_Manager grid_Manager;


    private void Awake()
    {
        GetGameObject = () => this.gameObject;
        grid_Manager = FindAnyObjectByType<Grid_Manager>();
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

        foreach (Transform i in Main_GameObject.transform)
        {
            if (i.TryGetComponent<A>(out A _component))
            {
                Component = _component;
                return true;
            }
        }

        return false;
    }

    public B FindComponnet_InChild<B>() where B : class
    {
        B _Component = null;

        foreach (Transform i in Main_GameObject.transform)
        {
            if (i.TryGetComponent<B>(out B _component))
            {
                _Component = _component;
                return _Component;
            }
        }
        return _Component;

    }
    #endregion
}
