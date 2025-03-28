using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level")]
public class Level_ScriptableObject : ScriptableObject
{



    List<SaveObjectTile> _level = new List<SaveObjectTile>();

    public List<SaveObjectTile> Level => _level;

    public void Save(List<SaveObjectTile> level)
    {
        _level = level;
    }
}
