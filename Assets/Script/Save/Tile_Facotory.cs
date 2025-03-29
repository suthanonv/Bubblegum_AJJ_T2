using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile_Facotory : MonoBehaviour
{
    [SerializeField] List<Tile_Type_class> tile_Type_Classes = new List<Tile_Type_class>();

    public Tile GetTilePrefeb(Tile runtimeTile)
    {
        if (runtimeTile == null) return null;

        foreach (var typeClass in tile_Type_Classes)
        {
            if (CompareTileComponents(runtimeTile.gameObject, typeClass.TilePrefeb.gameObject))
            {
                return typeClass.TilePrefeb;
            }
        }

        return null;
    }

    private bool CompareTileComponents(GameObject obj1, GameObject obj2)
    {
        Component[] comps1 = obj1.GetComponentsInChildren<Component>(true);
        Component[] comps2 = obj2.GetComponentsInChildren<Component>(true);

        // Compare by type only, ignoring component values
        var types1 = comps1.Select(c => c.GetType()).OrderBy(t => t.FullName).ToList();
        var types2 = comps2.Select(c => c.GetType()).OrderBy(t => t.FullName).ToList();

        if (types1.Count != types2.Count)
            return false;

        for (int i = 0; i < types1.Count; i++)
        {
            if (types1[i] != types2[i])
                return false;
        }

        return true;
    }
}

[System.Serializable]
public class Tile_Type_class
{
    public string TileType;
    public int ID;
    public Tile TilePrefeb;
}

