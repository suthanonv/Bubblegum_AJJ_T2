#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneAssetUtils
{
    public static SceneAsset GetSceneAssetByBuildIndex(int buildIndex)
    {
        if (buildIndex < 0 || buildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning("Invalid build index.");
            return null;
        }

        // Get scene path from build index
        string scenePath = SceneUtility.GetScenePathByBuildIndex(buildIndex);

        // Load the SceneAsset from path
        return AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
    }
}
#endif
