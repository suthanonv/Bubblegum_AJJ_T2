using UnityEngine;
using UnityEngine.Events;
public class MoveAble_Tile : Tile
{
    public MainComponent OcupiedObject { get; private set; }

    [SerializeField] UnityEvent<MainComponent> OnEnter;
    [SerializeField] UnityEvent<MainComponent> OnExit;


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
