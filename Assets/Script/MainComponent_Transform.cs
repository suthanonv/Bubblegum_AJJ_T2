using System;
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MainComponent_Transform))]
public class MainComponent_Transform : MonoBehaviour, IInitialize
{
    public Vector2Int currentTile_index { get; private set; } = new Vector2Int(-10, -10);
    Grid_Manager grid_Manager;

    MainComponent main;
    Direction _currentDirectionEnum;

    public Direction CurretionDirectionEnum => _currentDirectionEnum;

    Vector2Int _current_Direction = new Vector2Int(1, 0);
    public Vector2Int Current_direction => _current_Direction;

    public bool FreezeRotation { get; set; }


    private void Awake()
    {
        grid_Manager = FindAnyObjectByType<Grid_Manager>();

        main = this.GetComponent<MainComponent>();

        main.SetTransform(() => this);


        _GetDuration = () => 0.25f;
    }



    float Duration() => _GetDuration.Invoke();


    Func<float> _GetDuration;

    public void Set_GetOnMoveDuration_Func(Func<float> _duration_Func)
    {
        _GetDuration = _duration_Func;
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
                InstantSetPosition(currentTile_index);
                return;
            }
        }
    }
    Action onMove;
    Action onFinishMove;

    bool OnActive = false;
    public void Position(Vector2Int newPosition, Action OnMove = null, Action OnFinishMove = null)
    {
        if (OnActive)
        {
            this.transform.position = grid_Manager.Get_Tile(currentTile_index).transform.position;
            onFinishMove.Invoke();
            OnActive = false;
        }

        onMove = this.OnMove + OnMove;
        onFinishMove = this.OnFinishMove + OnFinishMove;



        OnActive = false;
        SetRotation(currentTile_index - newPosition);
        grid_Manager.Get_Tile(currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);
        Vector2 current_Pos = grid_Manager.Get_Tile(currentTile_index).transform.position;
        currentTile_index = newPosition;
        Vector2 _future_pos = grid_Manager.Get_Tile(currentTile_index).transform.position;

        StartCoroutine(Lerping_Move(current_Pos, _future_pos, Duration(), onMove, onFinishMove));
        grid_Manager.Get_Tile(newPosition).GetComponent<MoveAble_Tile>().SetOccupiedObject(main);
    }

    public void InstantSetPosition(Vector2Int newPosition)
    {
        currentTile_index = newPosition;

        MoveAble_Tile _future_tile = grid_Manager.Get_Tile(newPosition).GetComponent<MoveAble_Tile>();


        Vector2 _future_pos = _future_tile.gameObject.transform.position;

        transform.position = _future_pos;

        _future_tile.SetOccupiedObject(main);
        Invoke("RayCastSetPosAgain", 0.05f);
    }

    void RayCastSetPosAgain()
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

                MoveAble_Tile _future_tile = grid_Manager.Get_Tile(currentTile_index).GetComponent<MoveAble_Tile>();


                Vector2 _future_pos = _future_tile.gameObject.transform.position;

                transform.position = _future_pos;

                _future_tile.SetOccupiedObject(main);
                return;
            }
        }
    }
    public void SetRotation(Vector2Int Direction)
    {
        if (FreezeRotation) return;
        _current_Direction = Direction;
        _currentDirectionEnum = Vector2IntToDirect.ConvertVector2IntToDirection(Direction);
    }
    IEnumerator Lerping_Move(Vector2 _current_pos, Vector2 _future_pos, float duration, Action OnMove, Action OnFinishMove)
    {
        OnActive = true;
        float elapsedTime = 0f;

        OnMove?.Invoke();

        while (elapsedTime < duration)
        {
            transform.position = Vector2.Lerp(_current_pos, _future_pos, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = _future_pos;
        Debug.Log("On finish Lerping Call Onfinish Move");
        OnFinishMove?.Invoke();

        OnActive = false;
    }


    public void AddOnMoveListener(Action OnMoveCall)
    {
        OnMove += OnMoveCall;
    }

    public void RemoveOneMoveListener(Action OnMoveCall)
    {
        OnMove -= OnMoveCall;

    }

    Action OnFinishMove;
    Action OnMove;
    public void AddOnFinishMove(Action OnFinishMoveCall)
    {
        OnFinishMove += OnFinishMoveCall;
    }

    public void RemoveOnfinishMove(Action OnFinishMoveCall)
    {
        Debug.Log($"Removed {OnFinishMoveCall.Method.Name}");
        OnFinishMove -= OnFinishMoveCall;
    }



}
