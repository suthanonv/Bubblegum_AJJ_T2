using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomPropertyDrawer(typeof(SceneDropdownAttribute))]
public class SceneDropdownDrawer : PropertyDrawer
{
    private string[] sceneNames;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (sceneNames == null)
        {
            int sceneCount = EditorSceneManager.sceneCountInBuildSettings;
            sceneNames = new string[sceneCount];

            for (int i = 0; i < sceneCount; i++)
            {
                string path = SceneUtility.GetScenePathByBuildIndex(i);
                sceneNames[i] = Path.GetFileNameWithoutExtension(path);
            }
        }

        int index = Mathf.Max(0, System.Array.IndexOf(sceneNames, property.stringValue));
        index = EditorGUI.Popup(position, label.text, index, sceneNames);
        property.stringValue = sceneNames.Length > 0 ? sceneNames[index] : "";
    }
}
