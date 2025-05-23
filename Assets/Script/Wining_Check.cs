using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wining_Check : MonoBehaviour, I_SceneChange
{
    private List<Wining_Tile> wining_tiles = new();
    private bool _completedLevel = false;

    public event Action OnWin;
    public bool CompletedLevel => _completedLevel;

    private void Start()
    {
        LevelLoader.Instance.AddSceneChangeEvent(this);
        InputMove_Holder.Instance.OnFinishMove_AddListener(Check_isWining);

    }

    public void OnStartScene()
    {
        _completedLevel = false;
    }

    public void OnEndScene()
    {
        wining_tiles = new List<Wining_Tile>();
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
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        Level_Progress_Manager.Instance.SetSceneState(buildIndex, true);
        Level_Progress_Manager.Instance.SaveNow();

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
