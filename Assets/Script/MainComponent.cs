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
            Debug.Log($"Find Component {i.gameObject.name}");

            if (i.TryGetComponent<B>(out B _component))
            {
                _Component = _component;
                return _Component;
            }
        }
        Debug.Log($"Found nothing , so i try find component in child");
        _Component = Main_GameObject.transform.GetComponentInChildren<B>();

        Debug.Log($"{_Component == null} : Result of Serching Component");

        return _Component;


    }
    #endregion
}
