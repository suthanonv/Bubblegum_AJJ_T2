// Updated to avoid duplicate Path entries in writeHashSet
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class MoveObject_Manager : MonoBehaviour, bbg_IInitialize
{
    Grid_Manager gridManager;
    Vector2Int InputDirection = new Vector2Int(0, 0);
    List<Base_Movement> possibleMoveObject = new List<Base_Movement>();
    Dictionary<Vector2Int, bool> resolvedTile = new Dictionary<Vector2Int, bool>();
    [SerializeField] List<MovePath> DebugingPath = new List<MovePath>();

    private void Start()
    {
        this.GetComponent<InputMove_Holder>().Add_moveCall_Listener(MoveAll);
        gridManager = FindAnyObjectByType<Grid_Manager>(FindObjectsInactive.Include);
    }

    public int InitializeLayer() => 1;
    public Action Initialize()
    {
        Action action;
        action = () => { Setting(); };
        return action;
    }

    void Setting()
    {
        gridManager = FindAnyObjectByType<Grid_Manager>();
    }

    public void MoveAll(Vector2Int Direction, List<Base_Movement> moveGum, Action CallBack)
    {
        setUp(Direction, moveGum);
        GeneratePath(Direction, moveGum);
        CallBack?.Invoke();
    }

    void setUp(Vector2Int Direction, List<Base_Movement> moveGum)
    {
        movePath = new List<MovePath>();
        groupInfo = new List<ObjectGroup>();
        resolvedTile = new Dictionary<Vector2Int, bool>();
        DebugingPath = new List<MovePath>();
        InputDirection = Direction;
        possibleMoveObject = new List<Base_Movement>();
    }

    void GeneratePath(Vector2Int Direction, List<Base_Movement> moveGum)
    {
        HashSet<MovePath> ValidMove = new HashSet<MovePath>();
        HashSet<MovePath> InvalidMove = new HashSet<MovePath>();
        GenMovePathList(Direction, moveGum, ref ValidMove, ref InvalidMove);
        Move_vallid_Move(ValidMove, Direction);
        Interact_invalid_move(InvalidMove);
    }

    void GenMovePathList(Vector2Int Direction, List<Base_Movement> moveObjectInfo, ref HashSet<MovePath> ValidMove, ref HashSet<MovePath> InValidMove, Action callBack = null)
    {
        InValidMove = new HashSet<MovePath>();
        ValidMove = new HashSet<MovePath>();

        foreach (Base_Movement moveObject in moveObjectInfo)
        {
            HashSet<Base_Movement> visited = new HashSet<Base_Movement>();

            moveObjectCheck(Direction, moveObject, ref ValidMove, ref InValidMove, ref visited);
        }
    }

    [SerializeField] List<ObjectGroup> groupInfo = new List<ObjectGroup>();
    [SerializeField] List<MovePath> movePath = new List<MovePath>();
    ObjectGroup moveObjectCheck(Vector2Int Direction, I_move moveObject, ref HashSet<MovePath> ValidMove, ref HashSet<MovePath> InvalidMove, ref HashSet<Base_Movement> visited)
    {
        if (visited == null) visited = new HashSet<Base_Movement>();


        ObjectGroup serchGroup = groupInfo.FirstOrDefault(i => i.GroupMovePos.Contains(moveObject.DefaultPreMove(Direction)));

        if (serchGroup == null)
        {
            serchGroup = new ObjectGroup(moveObject.PremovePosition(Direction), moveObject.GetDirection(Direction));
            groupInfo.Add(serchGroup);
        }
        else
        {
            if (serchGroup.IsResolved) return serchGroup;
        }

        foreach (Vector2Int futureMove in serchGroup.GroupMovePos)
        {
            if (serchGroup.IsComplete(futureMove)) continue;
            MoveAble_Tile moveTile = gridManager.Get_Tile(futureMove - moveObject.GetDirection(Direction)).GetComponent<MoveAble_Tile>();

            if (moveTile.OcupiedObject == null)
            {
                continue;
            }
            Base_Movement objectOnTile = moveTile.OcupiedObject.FindComponnet_InChild<Base_Movement>();

            MoveAble_Tile NextTile = gridManager.Get_Tile(futureMove).GetComponent<MoveAble_Tile>();



            if (NextTile != null)
            {
                if (NextTile.OcupiedObject != null)
                {
                    Base_Movement NextObject = NextTile.OcupiedObject.FindComponnet_InChild<Base_Movement>();

                    if (NextObject != null)
                    {
                        Vector2Int nextObjectDirection = NextObject.DefaultPreMove(Direction);
                        if (serchGroup.GroupMovePos.Contains(NextObject.DefaultPreMove(Direction)))
                        {
                            Debug.Log("Found in the group");

                            if (serchGroup.IsComplete(nextObjectDirection))
                            {
                                serchGroup.AddFinish(futureMove, true);
                                continue;
                            }
                        }
                    }
                }

            }

            if (CheckMoveableTile(objectOnTile, Direction, ref ValidMove, ref InvalidMove, ref visited) == false)
            {
                // if false found overwrite all of the group to false
                Debug.Log("False");
                serchGroup.AddFinish(futureMove, false);
                writeHashSet(serchGroup, ref ValidMove, ref InvalidMove);
                return serchGroup;
            }
            else
            {
                serchGroup.AddFinish(futureMove, true);
            }

        }

        writeHashSet(serchGroup, ref ValidMove, ref InvalidMove);
        return serchGroup;

    }



    bool CheckMoveableTile(Base_Movement moveObject, Vector2Int Direction, ref HashSet<MovePath> ValidMove, ref HashSet<MovePath> InvalidMove, ref HashSet<Base_Movement> visited)
    {
        Tile rawTile = gridManager.Get_Tile(moveObject.DefaultPreMove(Direction));
        MoveAble_Tile nextMoveTile = rawTile.GetComponent<MoveAble_Tile>();
        if (nextMoveTile == null) return false;
        if (nextMoveTile.CanMove(moveObject.mainTransform.Main) == false) return false;
        if (nextMoveTile.OcupiedObject == null) return true;
        Base_Movement nextMoveObject = nextMoveTile.OcupiedObject.FindComponnet_InChild<Base_Movement>();
        if (nextMoveObject == null) return false;
        if ((nextMoveObject.Movelayer != -1 && moveObject.Movelayer != -1) && nextMoveObject.Movelayer != moveObject.Movelayer || nextMoveObject.GetDirection(Direction) != moveObject.GetDirection(Direction)) return false;
        bool calState = moveObjectCheck(Direction, nextMoveObject, ref ValidMove, ref InvalidMove, ref visited)._CompleteState;
        return calState;
    }

    void writeHashSet(ObjectGroup group, ref HashSet<MovePath> ValidMove, ref HashSet<MovePath> InvalidMove)
    {
        if (group.IsResolved == false) return;

        if (group._CompleteState == true)
        {
            foreach (Vector2Int futureMove in group.GroupMovePos)
            {
                Tile currentTile = gridManager.Get_Tile(futureMove - group.Direction);
                MoveAble_Tile currentMoveTile = currentTile.GetComponent<MoveAble_Tile>();
                if (currentMoveTile.OcupiedObject == null) continue;
                Base_Movement objectOnTile = currentMoveTile.OcupiedObject.FindComponnet_InChild<Base_Movement>();

                Tile nextTile = gridManager.Get_Tile(futureMove);
                MoveAble_Tile nextMoveTile = nextTile.GetComponent<MoveAble_Tile>();

                MovePath newPath = new MovePath(objectOnTile, group.Direction, nextTile, nextMoveTile);
                movePath.Add(newPath);
                ValidMove.Add(newPath);
            }
        }
        else
        {
            foreach (Vector2Int futureMove in group.GroupMovePos)
            {
                Tile currentTile = gridManager.Get_Tile(futureMove - group.Direction);
                MoveAble_Tile currentMoveTile = currentTile.GetComponent<MoveAble_Tile>();
                if (currentMoveTile.OcupiedObject == null) continue;
                Base_Movement objectOnTile = currentMoveTile.OcupiedObject.FindComponnet_InChild<Base_Movement>();

                Tile nextTile = gridManager.Get_Tile(futureMove);
                MoveAble_Tile nextMoveTile = nextTile.GetComponent<MoveAble_Tile>();


                MovePath newPath = new MovePath(objectOnTile, group.Direction, nextTile, nextMoveTile);
                movePath.Add(newPath);

                InvalidMove.Add(newPath);
            }
        }
    }

    void Move_vallid_Move(HashSet<MovePath> Valid_Move, Vector2Int Direction)
    {
        List<MovePath> sortedList = Valid_Move.OrderBy(p => p.Direction.x).ToList();

        if (Direction == new Vector2Int(0, 1)) sortedList = Valid_Move.OrderByDescending(i => i.CurrentPos.y).ToList();
        else if (Direction == new Vector2Int(0, -1)) sortedList = Valid_Move.OrderBy(i => i.CurrentPos.y).ToList();
        else if (Direction == new Vector2Int(1, 0)) sortedList = Valid_Move.OrderByDescending(i => i.CurrentPos.x).ToList();
        else if (Direction == new Vector2Int(-1, 0)) sortedList = Valid_Move.OrderBy(i => i.CurrentPos.x).ToList();

        foreach (MovePath path in sortedList)
        {
            path.PathOwner.Move(path.Direction);
        }
    }

    void Interact_invalid_move(HashSet<MovePath> InValid_Move)
    {
        foreach (MovePath Path in InValid_Move)
        {
            _Interact(Path);
            Path.PathOwner.OnInvalidMove();
        }
    }

    void _Interact(MovePath info)
    {

        if (info.MoveTile == null)
        {
            Object_Interactable _interact = info.WallTile.GetComponent<Object_Interactable>();
            if (_interact == null) return;
            _interact.Interact(info.PathOwner.mainTransform.Main);
        }
        else
        {
            if (info.PathOwner.mainTransform.Main.TryFindComponent_InChild<Object_Interactable>(out Object_Interactable moveInteract))
            {
                if (info.MoveTile.OcupiedObject != null)
                {
                    moveInteract.Interact(info.MoveTile.OcupiedObject);
                }
            }

            if (info.MoveTile.OcupiedObject != null)
            {
                Object_Interactable _interact = info.MoveTile.OcupiedObject.FindComponnet_InChild<Object_Interactable>();
                if (_interact == null) return;
                _interact.Interact(info.PathOwner.mainTransform.Main);
            }
        }
    }
}
[System.Serializable]
public class ObjectGroup
{
    Dictionary<Vector2Int, bool> CompleteIndex;
    public ObjectGroup(List<Vector2Int> GroupInfo, Vector2Int GroupDirection)
    {
        GroupMovePos = GroupInfo;
        CompleteIndex = new Dictionary<Vector2Int, bool>();
        Direction = GroupDirection;
        foreach (Vector2Int GroupPos in GroupInfo)
        {
            CompleteIndex[GroupPos] = false;
        }
    }


    public List<Vector2Int> GroupMovePos = new List<Vector2Int>();
    public Vector2Int Direction;

    [SerializeField] int CompleteCount;
    public bool _CompleteState;
    public bool IsResolved = false;

    public void AddFinish(Vector2Int Index, bool State)
    {
        if (State == true)
        {
            CompleteIndex[Index] = true;
            CompleteCount++;

            if (CompleteCount >= GroupMovePos.Count)
            {
                IsResolved = true;
                _CompleteState = true;
            }
        }
        else
        {
            foreach (Vector2Int GroupPos in GroupMovePos)
            {
                CompleteIndex[GroupPos] = true;
            }

            _CompleteState = false;
            IsResolved = true;
        }
    }

    public bool IsComplete(Vector2Int Index)
    {
        return CompleteIndex[Index];
    }
}


[System.Serializable]
public class MovePath
{
    public MovePath(Base_Movement pathOwner, Vector2Int Direction, Tile nextTile, MoveAble_Tile moveTile)
    {
        PathOwner = pathOwner;
        Name = pathOwner.mainTransform.Main.gameObject.name;
        direction = PathOwner.GetDirection(Direction);
        WallTile = nextTile;
        MoveTile = moveTile;
    }

    public string Name;
    public bool State;
    public Tile WallTile;
    public MoveAble_Tile MoveTile;
    public Base_Movement PathOwner;
    Vector2Int direction;
    public Vector2Int Direction => direction;
    public Vector2Int CurrentPos => PathOwner.DefaultPosition();

    public override bool Equals(object obj)
    {
        if (obj is not MovePath other) return false;
        return PathOwner == other.PathOwner && direction == other.direction;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PathOwner, direction);
    }
}
