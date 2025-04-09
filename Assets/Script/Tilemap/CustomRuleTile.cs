using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

[CreateAssetMenu]
public class CustomRuleTile : RuleTile<CustomRuleTile.Neighbor> {
    public bool alwaysConnect;
    public TileBase[] tilesToConnect;
    public TileBase tileToIgnore;
    public bool checkSelf;


    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int Any = 3;
        public const int SpecifiedList = 4;
        public const int Specific = 5;
        public const int Empty = 6;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch (neighbor) {
            case Neighbor.This: return CheckThis(tile);
            case Neighbor.NotThis: return CheckNotThis(tile);
            case Neighbor.Any: return CheckAny(tile);
            case Neighbor.SpecifiedList: return CheckSpecifiedList(tile);
            case Neighbor.Specific: return CheckSpecific(tile);
            case Neighbor.Empty: return CheckEmpty(tile);
        }
        return base.RuleMatch(neighbor, tile);
    }

    private bool CheckThis(TileBase tile)
    {
        if (!alwaysConnect) return tile == this;
        else return tilesToConnect.Contains(tile) || tile == this;
    }

    private bool CheckNotThis(TileBase tile)
    {
        return tile != this;
    }

    private bool CheckAny(TileBase tile)
    {
        if (checkSelf) return tile != null;
        else return tile != null && tile != this;
    }

    private bool CheckSpecifiedList(TileBase tile)
    {
        return tilesToConnect.Contains(tile);
    }

    private bool CheckSpecific(TileBase tile)
    {
        return tileToIgnore == tile;
    }

    private bool CheckEmpty(TileBase tile)
    {
        return tile == null;
    }
}