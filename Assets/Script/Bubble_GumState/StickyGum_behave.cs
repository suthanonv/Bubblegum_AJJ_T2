using UnityEngine;

public class StickyGum_behave : StateBehaviour<Bubble_Gum_State>
{
    public override Bubble_Gum_State state => Bubble_Gum_State.Stick;

    public override void OnEnterState()
    {
        base.OnEnterState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Add_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());

    }

    public override void OnExitState()
    {
        base.OnExitState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Remove_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());
        foreach (Transform i in this.transform)
        {
            if (i.gameObject.TryGetComponent<Attach_Moveable_List>(out Attach_Moveable_List attached))
            {
                attached.Reset_List();
            }
        }
        //Debug.Log($"{state} Exit");

    }
}
