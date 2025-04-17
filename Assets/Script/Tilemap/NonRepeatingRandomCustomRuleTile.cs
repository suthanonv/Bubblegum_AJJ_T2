using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class NonRepeatingRandomCustomRuleTile : RuleTile<NonRepeatingRandomCustomRuleTile.Neighbor>
{
    public Sprite[] randomSprites;
    private int tileSprite;

    public class Neighbor : RuleTile.TilingRuleOutput.Neighbor
    {
        // You can add custom neighbor checks here if needed
    }

    public override bool RuleMatch(int neighbor, TileBase tile)
    {
        // Default rule matching behavior
        return base.RuleMatch(neighbor, tile);
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        base.GetTileData(position, tilemap, ref tileData);

        if (randomSprites == null || randomSprites.Length == 0)
        {
            Debug.LogWarning("No random sprites assigned!");
            return;
        }

        // Check neighbors to avoid repetition
        bool isSameSprite = true;
        Vector3Int[] neighborOffsets = 
        {
            new Vector3Int(1, 0, 0),  // Right
            new Vector3Int(-1, 0, 0), // Left
            new Vector3Int(0, 1, 0),   // Up
            new Vector3Int(0, -1, 0)   // Down
        };

        foreach (var offset in neighborOffsets)
        {
            Vector3Int neighborPos = position + offset;
            TileBase neighborTile = tilemap.GetTile(neighborPos);

            if (neighborTile is NonRepeatingRandomCustomRuleTile neighborRuleTile)
            {
                if (neighborRuleTile.tileSprite == this.tileSprite)//if neighbour is the same sprite
                {
                    if (isSameSprite)
                    {
                        // Assign a random sprite and tile type
                        int randomIndex = Random.Range(0, randomSprites.Length);
                        tileData.sprite = randomSprites[randomIndex];
                        this.tileSprite = randomIndex; // Store the type for future checks
                    }
                    else
                    {
                        //If FALSE place random sprite instead of NULL
                        int randomIndex = Random.Range(0, randomSprites.Length);
                        tileData.sprite = randomSprites[randomIndex];
                        this.tileSprite = randomIndex; // Store the type for future checks

                        //tileData.sprite = null; // Don't place the tile
                    }
                }
            }
        }
    }
}