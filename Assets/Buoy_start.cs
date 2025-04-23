using UnityEngine;

public class Buoy_start : MonoBehaviour
{
    private void Awake()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;

        this.GetComponent<Animator>().enabled = false;
        FindAnyObjectByType<LevelLoader>().OnFinsihCloudLisener(EnableBuoy);
    }

    void EnableBuoy()
    {
        this.GetComponent<SpriteRenderer>().enabled = true;

        this.GetComponent<Animator>().enabled = true;
    }
}
