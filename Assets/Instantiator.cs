using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Instantiator : Singleton<Instantiator>
{
    [SerializeField] private string folderPath = "Assets/Prefab/SingletonObject";

    [SerializeField] private List<GameObject> Singleton_List = new List<GameObject>();

#if UNITY_EDITOR
    [ContextMenu("Load All Prefabs")]
    void LoadAllPrefabs()
    {
        Singleton_List.Clear();

        string[] guids = AssetDatabase.FindAssets("t:GameObject", new[] { folderPath });

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath);

            if (prefab != null)
            {
                Singleton_List.Add(prefab);
                Debug.Log("Loaded: " + prefab.name);
            }
        }
    }
#endif

    protected override void Init()
    {
        foreach (GameObject prefab in Singleton_List)
        {
            Instantiate(prefab);
            prefab.gameObject.name = $"{prefab.name}";
        }
    }
}
