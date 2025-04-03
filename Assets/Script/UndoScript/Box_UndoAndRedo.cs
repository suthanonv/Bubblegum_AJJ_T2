using System;
using System.Collections.Generic;
using UnityEngine;

public class Box_UndoAndRedo : UndoAndRedo<Box_UndoAndRedo.BoxSnapshot>
{
    [Serializable]
    public struct BoxSnapshot
    {
        public Vector2Int tileIndex;
    }

    private MainComponent_Transform movementComponent;

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
        movementComponent = GetComponent<MainComponent_Transform>();

        if (movementComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing MainComponent_Transform!");
    }

    public void RegisterState()
    {
        if (movementComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] Box RegisterState failed — missing component.");
            return;
        }

        var snapshot = new BoxSnapshot
        {
            tileIndex = movementComponent.currentTile_index
        };

        base.RegisterState(snapshot);
    }

    public void Undo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (UndoCount == 0) return;

        var snapshot = base.UndoState(GetCurrentSnapshot);
        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
    }

    public void Redo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (RedoCount == 0) return;

        var snapshot = base.RedoState(GetCurrentSnapshot);
        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
    }

    private BoxSnapshot GetCurrentSnapshot()
    {
        return new BoxSnapshot
        {
            tileIndex = movementComponent.currentTile_index
        };
    }
}
