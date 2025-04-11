using System.Collections.Generic;

public enum OBjectType { StickAble, WalkAble_Tile, Water, Box }

public class Tile_SpriteOrder
{
    static Dictionary<OBjectType, int> keyValuePairs =
        new Dictionary<OBjectType, int>() {
            { OBjectType.StickAble, 0 },
            { OBjectType.WalkAble_Tile, -99 },
            { OBjectType.Water, -100 },
        };



    public static int GetSpriteOrder(OBjectType Type)
    {
        if (keyValuePairs.ContainsKey(Type)) return keyValuePairs[Type];

        return int.MaxValue;
    }
}
