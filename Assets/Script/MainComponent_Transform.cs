using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MainComponent_Transform))]
public class MainComponent_Transform : MonoBehaviour, IInitialize
{
    public Vector2Int currentTile_index { get; private set; } = new Vector2Int(-10, -10);
    Grid_Manager grid_Manager;

    MainComponent main;

    private void Awake()
    {
        grid_Manager = FindAnyObjectByType<Grid_Manager>();

        main = this.GetComponent<MainComponent>();

        main.SetTransform(() => this);
    }


    private void Start()
    {
        Initialize().Invoke(); // will fixing in the future
    }

    public int InitializeLayer() => 2;
    public Action Initialize()
    {
        Action action;

        action = () => { Invoke("InitializeTiles", 0.5f); };

        return action;
    }


    public void InitializeTiles()
    {
        if (grid_Manager == null)
        {
            grid_Manager = FindAnyObjectByType<Grid_Manager>();
        }
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
            this.transform.position,
            new Vector2(0.1f, 0.1f),
            0,
            transform.up,
            10f,
            LayerMask.NameToLayer("Tile") // Ensure `layerMask` is defined properly
        );

        foreach (var hit in hits)
        {
            if (hit.collider.gameObject.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {
                currentTile_index = tile.Tile_Index;
                Position(currentTile_index);
                return;
            }
        }
    }

    public void Position(Vector2Int newPosition, Action OnMove = null, Action OnFinishMove = null)
    {
        grid_Manager.Get_Tile(currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);
        Vector2 current_Pos = grid_Manager.Get_Tile(currentTile_index).transform.position;
        currentTile_index = newPosition;
        Vector2 _future_pos = grid_Manager.Get_Tile(currentTile_index).transform.position;

        StartCoroutine(Lerping_Move(current_Pos, _future_pos, 0.15f, OnMove, OnFinishMove));
        grid_Manager.Get_Tile(newPosition).GetComponent<MoveAble_Tile>().SetOccupiedObject(main);
    }

    IEnumerator Lerping_Move(Vector2 _current_pos, Vector2 _future_pos, float duration, Action OnMove, Action OnFinishMove)
    {
        float elapsedTime = 0f;

        OnMove?.Invoke();

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(_current_pos, _future_pos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _future_pos;
        OnFinishMove?.Invoke();
    }

}
