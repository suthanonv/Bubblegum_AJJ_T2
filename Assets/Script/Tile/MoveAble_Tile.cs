using UnityEngine;
using UnityEngine.Events;
public class MoveAble_Tile : Tile
{
    public MainComponent OcupiedObject { get; private set; }

    [SerializeField] UnityEvent<MainComponent> OnEnter;
    [SerializeField] UnityEvent<MainComponent> OnExit;


    private void Awake()
    {
        SetUp_Delegate();
    }

    void SetUp_Delegate()
    {
        foreach (var i in gameObject.GetComponents<Grid_Collider>())
        {
            OnEnter.AddListener(i.OnEnter);
            OnExit.AddListener(i.OnExit);
        }
    }



    public void SetOccupiedObject(MainComponent _ocupiedObject)
    {
        this.OcupiedObject = _ocupiedObject;

        if (this.OcupiedObject == null)
        {
            OnExit.Invoke(_ocupiedObject);
        }
        else

        {
            OnEnter.Invoke(_ocupiedObject);
        }
    }
}
