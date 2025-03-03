using UnityEngine;

public class StickyGum_behave : StateBehaviour<Bubble_Gum_State>
{
    public override Bubble_Gum_State state => Bubble_Gum_State.Stick;

    public override void OnEnterState()
    {
        base.OnEnterState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Add_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());
        Debug.Log($"{state} enter");
    }

    public override void OnExitState()
    {
        base.OnExitState();
        FindAnyObjectByType<All_Sticky_Gum_Holder>().Remove_Sticky_Gum(this.transform.parent.gameObject.GetComponent<StateControl<Bubble_Gum_State>>());

        Debug.Log($"{state} Exit");

    }
}
