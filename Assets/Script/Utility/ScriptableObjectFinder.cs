using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectFinder : MonoBehaviour
{
    public static List<T> LoadAllScriptableObjectsInFolder<T>(string folderPath) where T : ScriptableObject
    {
        List<T> result = new List<T>();

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, new[] { folderPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (asset != null)
            {
                result.Add(asset);
            }
        }

        return result;
    }
}
