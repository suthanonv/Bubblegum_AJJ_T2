using System.Collections.Generic;
using UnityEngine;

public class UndoAndRedoController : Singleton<UndoAndRedoController>, I_SceneChange
{

    private BubbleGum_UndoManager bubblegum_undoManager;
    private Box_UndoAndRedo box_UndoAndRedo;
    TileManager tileManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LevelLoader.Instance.AddSceneChangeEvent(this);
    }

    public void OnStartScene()
    {
        StoreAllMoveableObject();
        tileManager = FindAnyObjectByType<TileManager>();
        bubblegum_undoManager = GetComponent<BubbleGum_UndoManager>();
        box_UndoAndRedo = GetComponent<Box_UndoAndRedo>();

        Input_handle.Instance.AddRegisterStateListener(RegisterAllCharacterStates);

        if (bubblegum_undoManager != null)
        {
            Debug.LogError($"{bubblegum_undoManager} is missing");
        }
        if (box_UndoAndRedo != null)
        {
            Debug.LogError($"{box_UndoAndRedo} is missing");
        }
    }

    public void OnEndScene()
    {

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
        SoundManager.PlaySound(SoundType.Effect_Undo, 1f);
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
            character.RegisterState(Get_Input.Instance.direction);
        }
        foreach (var character in Box_UndoAndRedo.AllBoxes)
        {
            character.RegisterState();
        }
    }

}
