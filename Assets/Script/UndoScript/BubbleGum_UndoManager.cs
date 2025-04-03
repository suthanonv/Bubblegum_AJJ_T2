using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BubbleGum_UndoManager : MonoBehaviour
{

    private Stack<CharacterSnapshot> undoStack = new Stack<CharacterSnapshot>();
    private Stack<CharacterSnapshot> redoStack = new Stack<CharacterSnapshot>();

    private MainComponent_Transform movementComponent;
    private Main_BubbleGumstate bubbleGumStateComponent;

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
        movementComponent = GetComponent<MainComponent_Transform>();
        bubbleGumStateComponent = GetComponent<Main_BubbleGumstate>();

        if (movementComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing MainComponent_Transform!");

        if (bubbleGumStateComponent == null)
            Debug.LogError($"[{gameObject.name}] is missing Main_BubbleGumstate!");
    }

    [Serializable]
    private struct CharacterSnapshot
    {
        public Vector2Int tileIndex;
        public Bubble_Gum_State state;
    }

    public void RegisterState()
    {
        if (movementComponent == null || bubbleGumStateComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RegisterState failed — Required component missing.");
            return;
        }

        var snapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState()
        };

        undoStack.Push(snapshot);
        redoStack.Clear();

        //Debug.Log($"[{gameObject.name}] Registered state: {snapshot.tileIndex}, {snapshot.state}");
    }

    public void UndoState(Action OnMove = null, Action OnFinishMove = null)
    {
        if (undoStack.Count == 0)
        {
            //Debug.LogWarning($"[{gameObject.name}] UndoState failed — Stack empty.");
            return;
        }

        var currentSnapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState()
        };

        redoStack.Push(currentSnapshot);
        var snapshot = undoStack.Pop();

        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        bubbleGumStateComponent.SetState(snapshot.state);

       // Debug.Log($"[{gameObject.name}] Undo to Tile: {snapshot.tileIndex}, State: {snapshot.state}");
    }

    public void RedoState(Action OnMove = null, Action OnFinishMove = null)
    {
        if (redoStack.Count == 0)
        {
            //Debug.LogWarning($"[{gameObject.name}] RedoState failed — Stack empty.");
            return;
        }

        var currentSnapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState()
        };

        undoStack.Push(currentSnapshot);
        var snapshot = redoStack.Pop();

        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        bubbleGumStateComponent.SetState(snapshot.state);

        //Debug.Log($"[{gameObject.name}] Redo to Tile: {snapshot.tileIndex}, State: {snapshot.state}");
    }


}
