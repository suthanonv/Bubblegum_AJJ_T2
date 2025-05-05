using UnityEngine;

public class Buoy_StartUp : MonoBehaviour
{


    private void Awake()
    {
        if (this.GetComponent<Animator>() != null)
        {
            this.GetComponent<Animator>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            this.GetComponentInChildren<Animator>().enabled = false;
            this.GetComponentInChildren<SpriteRenderer>().enabled = false;

        }


        FindAnyObjectByType<LevelLoader>().Add_FinishCloudAnimationListener(OnFinishCloudLoad);
    }

    void OnFinishCloudLoad()
    {
        if (this.GetComponent<Animator>() != null)
        {
            this.GetComponent<Animator>().enabled = true;
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            this.GetComponentInChildren<Animator>().enabled = true;
            this.GetComponentInChildren<SpriteRenderer>().enabled = true;

        }

    }
}