public class Main_BubbleGumstate : StateControl<Bubble_Gum_State>
{




    public override void SetState(Bubble_Gum_State _newState)
    {
        if (_newState == GetCurrentState()) { return; }
        base.SetState(_newState);
    }



}
