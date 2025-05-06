using UnityEngine;

public class BBG_Reset_AddingSelf : MonoBehaviour
{
    Attach_Moveable_List Attach_Moveable_List;

    private void Awake()
    {
        Attach_Moveable_List = this.GetComponent<Attach_Moveable_List>();
    }

    void Start()
    {
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Add_OnUnStickGum(ResetSetUp);
    }


    void ResetSetUp()
    {
        if (Attach_Moveable_List._AddingSelfSetUp == false)
        {
            Attach_Moveable_List._AddingSelfSetUp = true;
        }
    }

}
