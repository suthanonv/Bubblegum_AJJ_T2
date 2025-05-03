using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BackGroundMusicAdjust
{
    public AudioClip BGM;

    [Tooltip("List all scene names this BGM should play in.")]
    public List<string> SceneNames;
}
