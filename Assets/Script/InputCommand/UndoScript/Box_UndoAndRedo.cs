using System;
using System.Collections.Generic;
using UnityEngine;

public class Box_UndoAndRedo : UndoAndRedo<Box_UndoAndRedo.BoxSnapshot>
{
    [Serializable]
    public struct BoxSnapshot
    {
        public Vector2Int tileIndex;
        public List<GameObject> attachedObjectList;
        public Vector2Int directionFrom;
    }

    [SerializeField] MainComponent_Transform movementComponent;
    [SerializeField] private Attach_Moveable_List attach_Moveable_List;
    

    public static List<Box_UndoAndRedo> AllBoxes = new List<Box_UndoAndRedo>();

    private void OnEnable()
    {
        if (!AllBoxes.Contains(this))
            AllBoxes.Add(this);
    }

    private void OnDisable()
    {
        if (AllBoxes.Contains(this))
            AllBoxes.Remove(this);
    }

    private void Awake()
    {

        if (movementComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing MainComponent_Transform!");
        
    }

    public void RegisterState()
    {
        if (movementComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RegisterState failed — Required component missing.");
            return;
        }

        var fullGroup = attach_Moveable_List.Get_List();
        List<GameObject> attachedObjects = new List<GameObject>();

        foreach (var move in fullGroup)
        {
            if (move != null)
                attachedObjects.Add(move.gameObject);
        }

        var snapshot = new BoxSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            attachedObjectList = attachedObjects,
        };

        base.RegisterState(snapshot);
    }

    public void Undo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (UndoCount == 0) return;

        var snapshot = base.UndoState(GetCurrentSnapshot);
        //movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        movementComponent.Position(snapshot.tileIndex);
        RestoreAttachment(snapshot.attachedObjectList);
    }

    public void Redo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (RedoCount == 0) return;

        var snapshot = base.RedoState(GetCurrentSnapshot);
        //movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        movementComponent.Position(snapshot.tileIndex);
        RestoreAttachment(snapshot.attachedObjectList);
    }

    private BoxSnapshot GetCurrentSnapshot() 
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

        return new BoxSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
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

        Debug.Log($"{movementComponent.name} [RestoreAttachment] Group restored with {attachMoveables.Count} objects");
    }
}
