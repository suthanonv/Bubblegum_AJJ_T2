public class MoveAble_Tile : Tile
{
    public MainComponent OcupiedObject { get; private set; }



    public void SetOccupiedObject(MainComponent _ocupiedObject)
    {
        this.OcupiedObject = _ocupiedObject;
    }
}
