using UnityEngine;

public class Space_Deco_Buoy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<All_Sticky_Gum_Holder>().AddOnBubbleGumStickEvent(Active_Deco);
    }

    private void Awake()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
    }

    void Active_Deco()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
    }
}
