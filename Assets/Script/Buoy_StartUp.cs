using UnityEngine;

public class Buoy_StartUp : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<Animator>().enabled = false;
        this.GetComponent<SpriteRenderer>().enabled = false;

        FindAnyObjectByType<LevelLoader>().Add_FinishCloudAnimationListener(OnFinishCloudLoad);
    }

    void OnFinishCloudLoad()
    {
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<SpriteRenderer>().enabled = true;
    }
}
