using System;
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
        this.GetComponent<SpriteRenderer>().sortingOrder = Tile_SpriteOrder.GetSpriteOrder(OBjectType.WalkAble_Tile);
    }

    void SetUp_Delegate()
    {
        foreach (var i in gameObject.GetComponents<Grid_Collider>())
        {
            OnEnter.AddListener(i.OnEnter);
            OnExit.AddListener(i.OnExit);
        }
    }

    Func<MainComponent, bool> CanMoveConditon;

    public bool CanMove(MainComponent _object)
    {
        return CanMoveConditon?.Invoke(_object) ?? true;
    }

    public void Add_tile_Condition(Func<MainComponent, bool> _new)
    {
        CanMoveConditon = _new;
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
            SoundManager.PlaySound(SoundType.BBG_Land);
            OnEnter.Invoke(_ocupiedObject);
        }
    }
}
