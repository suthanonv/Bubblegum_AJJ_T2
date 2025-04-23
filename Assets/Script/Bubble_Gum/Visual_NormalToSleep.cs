public class Visual_NormalToSleep : StateTransition<Bubble_Gum_State>
{
    protected override Bubble_Gum_State CurrentState => Bubble_Gum_State.Normal;

    protected override Bubble_Gum_State _nextState => Bubble_Gum_State.Sleep;
}
