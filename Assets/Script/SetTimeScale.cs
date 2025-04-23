using UnityEngine;

public class SetTimeScale : MonoBehaviour
{
    [SerializeField] float TimeScale = 1;
    void Start()
    {
        Time.timeScale = TimeScale;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
