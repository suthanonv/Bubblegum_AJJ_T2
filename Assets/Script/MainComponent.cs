using System;
using System.Collections;
using UnityEngine;
public class MainComponent : MonoBehaviour
{
    public Vector2Int currentTile_index { get; private set; } = new Vector2Int(-10, -10);

    Grid_Manager grid_Manager;


    private void Awake()
    {
        GetGameObject = () => this.gameObject;
        grid_Manager = FindAnyObjectByType<Grid_Manager>();

    }
    private void Start()
    {
        Invoke("InitializeTile", 0.1f);
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
                Debug.Log($"{tile.name} : {tile.Tile_Index}");
                currentTile_index = tile.Tile_Index;
                Position(currentTile_index);
                return;
            }
        }
    }


    public void Position(Vector2Int newPosition)
    {

        grid_Manager.Get_Tile(currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);
        Vector2 current_Pos = grid_Manager.Get_Tile(currentTile_index).transform.position;
        currentTile_index = newPosition;
        Vector2 _future_pos = grid_Manager.Get_Tile(currentTile_index).transform.position;

        StartCoroutine(Lerping_Move(current_Pos, _future_pos, 0.05f));
        grid_Manager.Get_Tile(newPosition).GetComponent<MoveAble_Tile>().SetOccupiedObject(this);
    }



    IEnumerator Lerping_Move(Vector2 _current_pos, Vector2 _future_pos, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(_current_pos, _future_pos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _future_pos; // Ensure it reaches the exact position at the end
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
