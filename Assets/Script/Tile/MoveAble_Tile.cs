using UnityEngine;

public class MoveAble_Tile : MonoBehaviour
{
    public MainComponent OcupiedObject { get; private set; }

    public bool CanMoveTo()
    {
        return OcupiedObject == null;
    }

    public void SetOccupiedObject(MainComponent _ocupiedObject)
    {
        this.OcupiedObject = _ocupiedObject;
    }
}
