using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PrefabID : Singleton<PrefabID>
{
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();

    public GameObject GetPrefab(int ID)
    {
        return prefabs[ID];
    }
}
