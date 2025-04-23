using UnityEngine;

public class CurrentLevelClass : MonoBehaviour
{
    public static int CurrentLevel { get; private set; } = 0;


    public static void SetNewLevel(int _currentLevel)
    {
        CurrentLevel = _currentLevel;
    }
}
