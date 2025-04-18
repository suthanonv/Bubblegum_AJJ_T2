using System.Collections.Generic;
using UnityEngine;

public class UndoAndRedoController : MonoBehaviour
{
    [SerializeField] private Input_handle inputHandle;
    [SerializeField] private Get_Input get_input;
    private BubbleGum_UndoManager bubblegum_undoManager;
    private Box_UndoAndRedo box_UndoAndRedo;
    TileManager tileManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StoreAllMoveableObject();
        tileManager = FindAnyObjectByType<TileManager>();
        bubblegum_undoManager = GetComponent<BubbleGum_UndoManager>();
        box_UndoAndRedo = GetComponent<Box_UndoAndRedo>();
        if (inputHandle != null)
        {
            inputHandle.AddRegisterStateListener(RegisterAllCharacterStates);
        }
        if (bubblegum_undoManager != null)
        {
            Debug.LogError($"{bubblegum_undoManager} is missing");
        }
        if (box_UndoAndRedo != null)
        {
            Debug.LogError($"{box_UndoAndRedo} is missing");
        }
    }


    List<MainComponent_Transform> moveableObjectList = new List<MainComponent_Transform>();
    void StoreAllMoveableObject()
    {
        moveableObjectList.Clear(); // Optional: clear the list first if reusing
        foreach (var i in FindObjectsByType<MainComponent_Transform>(FindObjectsSortMode.None))
        {
            moveableObjectList.Add(i);
        }

    }

    void initializeAllObject()
    {
        foreach (var i in moveableObjectList)
        {
            i.InitializeTiles();
        }
    }

    public bool EnableUndo { get; set; } = true;

    public void Undo()
    {
        if (EnableUndo == false) return;
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.Undo();
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.Undo();
        }

        tileManager.ResetObjectOnTiles(initializeAllObject);
    }
    public void Redo()
    {


        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.Redo();
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.Redo();
        }
    }

    private void RegisterAllCharacterStates()
    {
        foreach (var character in BubbleGum_UndoManager.AllCharacters)
        {
            character.RegisterState(get_input.direction);
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.RegisterState();
        }
    }

}
