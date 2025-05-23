using UnityEngine;

public class BBG_Reset_AddingSelf : MonoBehaviour
{
    Grouping Attach_Moveable_List;

    private void Awake()
    {
        Attach_Moveable_List = this.GetComponent<Grouping>();
    }

    void Start()
    {
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Add_OnUnStickGum(ResetSetUp);
    }


    void ResetSetUp()
    {
        if (Attach_Moveable_List._AddingSelf == false)
        {
            Attach_Moveable_List._AddingSelf = true;
        }
    }

}
