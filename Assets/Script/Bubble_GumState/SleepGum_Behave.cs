using UnityEngine;
public class SleepGum_Behave : StateBehaviour<Bubble_Gum_State>
{

    public override Bubble_Gum_State state => Bubble_Gum_State.Sleep;

    public override void OnEnterState()
    {
        Debug.Log($"{state} enter");
    }

    public override void OnExitState()
    {
        Debug.Log($"{state} Exit");

    }
}
