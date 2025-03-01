using UnityEngine;

public class StickyGum_behave : StateBehaviour<Bubble_Gum_State>
{
    public override Bubble_Gum_State state => Bubble_Gum_State.Stick;

    public override void OnEnterState()
    {
        base.OnEnterState();
        Debug.Log($"{state} enter");
    }

    public override void OnExitState()
    {
        base.OnExitState();
        Debug.Log($"{state} Exit");

    }
}
