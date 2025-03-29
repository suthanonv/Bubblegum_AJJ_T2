using System.Collections.Generic;
using UnityEngine;

public class SaveLevel : MonoBehaviour
{
    [SerializeField] Level_ScriptableObject Level_data;


    public void Save()
    {
        List<SaveObjectTile> allTiles = new List<SaveObjectTile>();


        Level_data.Save(allTiles);
    }

}
