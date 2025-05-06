using UnityEngine;

public class StickyGum_behave : StateBehaviour<Bubble_Gum_State>
{
    public override Bubble_Gum_State state => Bubble_Gum_State.Stick;

    public override void OnEnterState()
    {
        SoundManager.PlaySound(SoundType.BBG_Stick, 1f);

        base.OnEnterState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Add_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());
        if (this.transform.parent.GetComponent<MainComponent_Transform>().Current_direction.x == 0 && this.transform.parent.GetComponent<MainComponent_Transform>().Current_direction.y == 0)
        {
            if (this.transform.GetComponentInChildren<Attach_Moveable_List>()._AddingSelfSetUp == false)
            {
                this.transform.GetComponentInChildren<Attach_Moveable_List>()._AddingSelfSetUp = true;
            }
        }
    }

    public override void OnExitState()
    {
        SoundManager.PlaySound(SoundType.BBG_Unstick, 1f);

        base.OnExitState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Remove_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());
        foreach (Transform i in this.transform)
        {
            if (i.gameObject.TryGetComponent<Attach_Moveable_List>(out Attach_Moveable_List attached))
            {
                attached.Reset_List();
            }
        }

    }
}
