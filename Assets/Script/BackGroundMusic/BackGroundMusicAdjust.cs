using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BackGroundMusicAdjust : ScriptableObject
{
    [SceneDropdown]
    public List<string> SceneThatUsethisMusic = new List<string>();
    public AudioClip BGM;


}
