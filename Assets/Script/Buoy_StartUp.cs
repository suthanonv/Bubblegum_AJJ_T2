using UnityEngine;
using System.Collections;

public class Buoy_StartUp : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        
    }
    private void Start()
    {
        StartDelayedActivation();
    }

    public void StartDelayedActivation()
    {
        StartCoroutine(EnableAfterDelay());
    }

    private IEnumerator EnableAfterDelay()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Animator>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }
}
