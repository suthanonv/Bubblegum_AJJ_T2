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
    }


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

    public void RegisterState()
    {
        if (movementComponent == null || bubbleGumStateComponent == null)
        {
            Debug.LogWarning($"[{gameObject.name}] RegisterState failed — Required component missing.");
            return;
        }

        var attachList = GetComponent<Attach_Moveable_List>()?.Get_List();
        List<GameObject> attachedObjects = new List<GameObject>();

        if (attachList != null)
        {
            foreach (var move in attachList)
            {
                if (move != null && move.gameObject != this.gameObject)
                    attachedObjects.Add(move.gameObject);
            }
        }

        var snapshot = new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState(),
            attachedObjectList = attachedObjects
        };

        base.RegisterState(snapshot);
    }


    public void Undo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (UndoCount == 0) return;

        var snapshot = base.UndoState(GetCurrentSnapshot);
        
        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        bubbleGumStateComponent.SetState(snapshot.state);

        if (snapshot.attachedObjectList == null || snapshot.attachedObjectList.Count == 0)
            return;

        RestoreAttachment(snapshot.attachedObjectList);
    }


    public void Redo(Action OnMove = null, Action OnFinishMove = null)
    {
        if (RedoCount == 0) return;

        var snapshot = base.RedoState(GetCurrentSnapshot);
        RestoreAttachment(snapshot.attachedObjectList);
        movementComponent.Position(snapshot.tileIndex, OnMove, OnFinishMove);
        bubbleGumStateComponent.SetState(snapshot.state);

        if (snapshot.attachedObjectList == null || snapshot.attachedObjectList.Count == 0)
            return;

        RestoreAttachment(snapshot.attachedObjectList);
    }


    private CharacterSnapshot GetCurrentSnapshot()
    {
        return new CharacterSnapshot
        {
            tileIndex = movementComponent.currentTile_index,
            state = bubbleGumStateComponent.GetCurrentState()
        };
    }
    private void RestoreAttachment(List<GameObject> previousAttachList)
    {
        var selfAttach = GetComponent<Attach_Moveable_List>();
        if (selfAttach == null) return;

        List<Attach_Moveable_List> attachMoveables = new List<Attach_Moveable_List> { selfAttach };

        foreach (var obj in previousAttachList)
        {
            var attach = obj?.GetComponent<Attach_Moveable_List>();
            if (attach != null)
                attachMoveables.Add(attach);
        }

        selfAttach.Add_New_Moveable(attachMoveables);
    }

}
