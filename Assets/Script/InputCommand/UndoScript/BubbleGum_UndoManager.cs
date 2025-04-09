using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        
    }

    public void RegisterState(Vector2Int direction)
    {
        if (movementComponent == null || bubbleGumStateComponent == null || attach_Moveable_List == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RegisterState failed — Required component missing.");
            return;
        }

        var group = attach_Moveable_List.Get_List();
        List<GameObject> attachedObjects = new List<GameObject>();

        foreach (var move in group)
        {
            if (move != null)
                attachedObjects.Add(move.gameObject);
        }

        var snapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState(),
            attachedObjectList = attachedObjects,
            directionFrom = direction 
        };

        base.RegisterState(snapshot);
    }


    public void Undo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (UndoCount == 0) return;

        var snapshot = base.UndoState(GetCurrentSnapshot);

        bubbleGumStateComponent.SetState(snapshot.state);
        RestoreAttachment(snapshot.attachedObjectList);

        
        Vector2Int backDirection = snapshot.directionFrom;
        movementComponent.SetRotation(backDirection);

        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
    }


    public void Redo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (RedoCount == 0) return;

        var snapshot = base.RedoState(GetCurrentSnapshot);

        bubbleGumStateComponent.SetState(snapshot.state);
        RestoreAttachment(snapshot.attachedObjectList);
        
        movementComponent.SetRotation(snapshot.directionFrom);
        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
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
            if (attach != null)
                attachMoveables.Add(attach);
        }

        foreach (var item in attachMoveables)
        {
            item.Set_Same_list(attachMoveables);
        }

        Debug.Log($"[RestoreAttachment] Group restored with {attachMoveables.Count} objects");
    }
}
