using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGum_UndoManager : UndoAndRedo<BubbleGum_UndoManager.CharacterSnapshot>
{
    [Serializable]
    public struct CharacterSnapshot
    {
        public Vector2Int tileIndex;
        public Bubble_Gum_State state;
        public List<GameObject> attachedObjectList;
        public Vector2Int directionFrom;
    }

    Grid_Manager gridManage;

    [SerializeField] private Move_All moveAll;
    [SerializeField] private MainComponent_Transform movementComponent;
    [SerializeField] private Main_BubbleGumstate bubbleGumStateComponent;
    [SerializeField] private Attach_Moveable_List attach_Moveable_List;

    public static List<BubbleGum_UndoManager> AllCharacters = new List<BubbleGum_UndoManager>();

    private void OnEnable()
    {
        if (!AllCharacters.Contains(this))
            AllCharacters.Add(this);
    }

    private void OnDisable()
    {
        if (AllCharacters.Contains(this))
            AllCharacters.Remove(this);
    }

    private void Awake()
    {

        if (movementComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing MainComponent_Transform!");
        if (bubbleGumStateComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing Main_BubbleGumstate!");

        gridManage = FindAnyObjectByType<Grid_Manager>();

    }

    public void RegisterState(Vector2Int direction)
    {
        if (movementComponent == null || bubbleGumStateComponent == null || attach_Moveable_List == null)
        {
            Debug.LogWarning($"[{bubbleGumStateComponent.gameObject.name}] RegisterState failed — Required component missing.");
            return;
        }


        var group = attach_Moveable_List.Get_List();
        List<GameObject> attachedObjects = new List<GameObject>();
        Debug.Log($"Debuging Gum State{movementComponent.gameObject.name} , {bubbleGumStateComponent.GetCurrentState()} , {group.Count}");

        foreach (var move in group)
        {
            if (move != null)
            {
                attachedObjects.Add(move.gameObject);


            }

        }

        var snapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState(),
            attachedObjectList = attachedObjects,

            directionFrom = movementComponent.Current_direction
        };

        base.RegisterState(snapshot);
    }


    public void Undo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (UndoCount == 0) return;
        gridManage.Get_Tile(movementComponent.currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);

        var snapshot = base.UndoState(GetCurrentSnapshot);
        if (movementComponent.currentTile_index != snapshot.tileIndex)
            gridManage.Get_Tile(movementComponent.currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);
        bubbleGumStateComponent.SetState(snapshot.state);
        RestoreAttachment(snapshot.attachedObjectList);


        Vector2Int backDirection = snapshot.directionFrom;
        movementComponent.SetRotation(backDirection);

        movementComponent.InstantSetPosition(snapshot.tileIndex);
        //movementComponent.InstantSetPosition(snapshot.tileIndex);

        //moveAll.MoveAll(backDirection, Move.Get_List().Select(g => g.GetComponent<Movement>()).ToList(), OnFinishMove);

    }


    public void Redo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (RedoCount == 0) return;
        gridManage.Get_Tile(movementComponent.currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);

        var snapshot = base.RedoState(GetCurrentSnapshot);
        if (movementComponent.currentTile_index != snapshot.tileIndex)
            gridManage.Get_Tile(movementComponent.currentTile_index).GetComponent<MoveAble_Tile>().SetOccupiedObject(null);
        bubbleGumStateComponent.SetState(snapshot.state);
        RestoreAttachment(snapshot.attachedObjectList);

        movementComponent.SetRotation(snapshot.directionFrom);
        movementComponent.InstantSetPosition(snapshot.tileIndex);
        //movementComponent.InstantSetPosition(snapshot.tileIndex);
        //moveAll.MoveAll(snapshot.tileIndex, attach_Moveable_List.Get_List().Select(g => g.GetComponent<Movement>()).ToList(), OnFinishMove);
    }

    private CharacterSnapshot GetCurrentSnapshot()
    {
        var group = attach_Moveable_List?.Get_List();
        List<GameObject> attachedObjects = new List<GameObject>();

        if (group != null)
        {
            foreach (var move in group)
            {
                if (move != null)
                    attachedObjects.Add(move.gameObject);
            }
        }

        return new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState(),
            attachedObjectList = attachedObjects
        };
    }

    private void RestoreAttachment(List<GameObject> previousAttachList)
    {
        if (previousAttachList == null || previousAttachList.Count == 0) return;
        List<Attach_Moveable_List> attachMoveables = new List<Attach_Moveable_List>();

        foreach (var obj in previousAttachList)
        {
            var attach = obj?.GetComponent<Attach_Moveable_List>();
            attachMoveables.Add(attach);
        }


        foreach (var item in attachMoveables)
        {
            item.Set_Same_list(attachMoveables);
            Debug.Log("มึงนี่เองไอเหี้ย RestoreAttachment ของ Bubblegum ");
        }

        Debug.Log($"{movementComponent.name} [RestoreAttachment] Group restored with {attachMoveables.Count}, objects");
    }
}
