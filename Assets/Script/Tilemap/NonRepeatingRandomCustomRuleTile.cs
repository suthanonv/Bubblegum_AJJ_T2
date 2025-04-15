using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class NonRepeatingRandomCustomRuleTile : RuleTile<NonRepeatingRandomCustomRuleTile.Neighbor> {
    public Sprite[] randomSprites;

    public class Neighbor : RuleTile.TilingRuleOutput.Neighbor
    {
        public const int DontMatch = 3;
    }

    public void bool GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // Grab neighbor sprites
        HashSet<Sprite> neighborSprites = new HashSet<Sprite>();

        foreach (Vector3Int offset in RuleTile.TilingRule.neighborPositions)
        {
            TileBase neighborTile = tilemap.GetTile(position + offset);
            if (neighborTile is RuleTile neighborRuleTile)
            {
                TileData neighborData = new TileData();
                neighborRuleTile.GetTileData(position + offset, tilemap, ref neighborData);
                if (neighborData.sprite != null)
                {
                    neighborSprites.Add(neighborData.sprite);
                }
            }
        }

        // Pick a sprite that isn't in neighbors
        List<Sprite> options = new List<Sprite>();
        foreach (Sprite sprite in randomSprites)
        {
            if (!neighborSprites.Contains(sprite))
            {
                options.Add(sprite);
            }
        }

        // Fallback if all are taken
        Sprite selected = options.Count > 0
            ? options[Random.Range(0, options.Count)]
            : randomSprites[Random.Range(0, randomSprites.Length)];

        tileData.sprite = selected;
        tileData.colliderType = this.m_DefaultSpriteColliderType;

        return true;
    }
}