using UnityEngine;

public class BuoyWASDStartup : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;

    }
    private void Update()
    {
        Startup();
    }

    public void Startup()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }

   
}
