using System.Collections.Generic;
using UnityEngine;

public class SaveLevel : MonoBehaviour
{
    [SerializeField] Level_ScriptableObject Level_data;

    private void Start()
    {
        Save();
    }
    public void Save()
    {
        List<SaveObjectTile> allTiles = new List<SaveObjectTile>();

        allTiles.Add(new SaveObjectTile(new Vector2Int(0, 0), null, null));

        Level_data = new Level_ScriptableObject(allTiles);
    }

}
