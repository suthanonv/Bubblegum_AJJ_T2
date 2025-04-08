using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }

public class Vector2IntToDirect : MonoBehaviour
{
    static Dictionary<Vector2Int, Direction> _vector2ToDirection = new Dictionary<Vector2Int, Direction>() {
        {new Vector2Int(0,1) , Direction.Up } ,
        { new Vector2Int(0 , -1) , Direction.Down},
        {new Vector2Int(1  , 0) , Direction.Right },
        {new Vector2Int(-1 , 0) , Direction.Left }
        };


    public static Direction ConvertVector2IntToDirection(Vector2Int _vector)
    {
        if (_vector2ToDirection.ContainsKey(_vector) == false) return Direction.Up;

        return _vector2ToDirection[_vector];
    }
}
