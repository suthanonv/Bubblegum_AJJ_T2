using UnityEngine;

public class GameSystem : Singleton<GameSystem>
{
    protected override void Init()
    {
        Debug.Log("GameManager initialized!");
    }

}
