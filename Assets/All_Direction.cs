using System.Collections.Generic;
using UnityEngine;

public class All_Direction : MonoBehaviour
{
    public static readonly List<Vector2Int> Directions = new List<Vector2Int>() { new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(1, 0), new Vector2Int(-1, 0) };
}
