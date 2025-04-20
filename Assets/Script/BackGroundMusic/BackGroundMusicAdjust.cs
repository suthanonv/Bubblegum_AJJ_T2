using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class BackGroundMusicAdjust : ScriptableObject
{
    public List<string> SceneThatUsethisMusic = new List<string>();
    public AudioClip BGM;


}
