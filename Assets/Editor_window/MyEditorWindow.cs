using UnityEditor;
using UnityEngine;

public class MyEditorWindow : EditorWindow
{
    // Add menu item to open the window
    [MenuItem("Window/Initialize scene")]
    public static void ShowWindow()
    {
        // Get existing open window or create a new one
        GetWindow<MyEditorWindow>("Initialize scene");
    }

    private void OnGUI()
    {
        GUILayout.Label("Initialize Tile and Object!", EditorStyles.boldLabel);

        if (GUILayout.Button("Click Me"))
        {
            Initializer.ExecuteInitialize();
        }
    }
}
