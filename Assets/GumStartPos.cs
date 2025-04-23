using UnityEngine;

public class GumStartPos : MonoBehaviour
{
    private void Awake()
    {
        foreach (var i in FindObjectsByType<LevelSelectTile>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            if (i.LelSelect == CurrentLevelClass.CurrentLevel)
            {
                var BubbleGum = FindAnyObjectByType<MainComponent_Transform>();

                if (BubbleGum != null)
                {
                    BubbleGum.transform.position = i.transform.position;
                }
            }
        }
    }
}
