using System;
using System.Collections.Generic;
using UnityEngine;

public class Wining_Check : MonoBehaviour
{
    private readonly List<Wining_Tile> wining_tiles = new();
    private bool _completedLevel = false;

    public event Action OnWin;
    public bool CompletedLevel => _completedLevel;

    private void Start()
    {
        var gumHolder = FindAnyObjectByType<InputMove_Holder>();
        if (gumHolder != null)
        {
            gumHolder.OnFinishMove_AddListener(Check_isWining);
        }
        else
        {
            Debug.LogError("[Wining_Check] Cannot find All_moveable_gum_holder in scene.");
        }
    }

    public void Add_WiningTile(Wining_Tile tile)
    {
        if (tile != null && !wining_tiles.Contains(tile))
        {
            wining_tiles.Add(tile);
        }
    }

    public void Check_isWining()
    {
        if (_completedLevel || wining_tiles.Count == 0) return;

        foreach (var tile in wining_tiles)
        {
            if (tile == null || !tile.IsWin)
            {
                return;
            }
        }


        _completedLevel = true;
        SoundManager.PlaySound(SoundType.Effect_Winning);
        Debug.Log("[Wining_Check] All tiles are in win state. Broadcasting victory.");
        OnWin?.Invoke();
    }


    public void OnDisableWinTile_Sprite()
    {
        foreach (var tile in wining_tiles)
        {
            tile.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        }

    }
}
