using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
