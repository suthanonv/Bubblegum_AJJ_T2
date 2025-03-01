using System;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    public Vector2Int currentTile_index { get; private set; } = new Vector2Int(-10, -10);

    Grid_Manager grid;

    private void Start()
    {
        grid = FindAnyObjectByType<Grid_Manager>();
        InitializeTile();
    }
    void InitializeTile()
    {
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            this.transform.position,
            this.transform.localScale,
            0,
            transform.up,
            10f,
            LayerMask.NameToLayer("Tile") // Ensure `layerMask` is defined properly
        );

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {
                tile.SetOccupiedObject(this);

            }
        }
    }


    public void Position(Vector2Int newPosition)
    {
        if (currentTile_index != new Vector2Int(-10, -10))
        {
            grid.Get_Tile(newPosition).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);

        }
        currentTile_index = newPosition;
        this.transform.position = grid.Get_Tile(newPosition).transform.position;
        grid.Get_Tile(newPosition).GetComponent<MoveAble_Tile>().SetOccupiedObject(this);
    }


    Func<GameObject> GetGameObject = () => null;

    public void GameObject_SetFunc(Func<GameObject> newFunc)
    {
        GetGameObject = newFunc;
    }

    public GameObject Main_GameObject => GetGameObject?.Invoke();

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

}
