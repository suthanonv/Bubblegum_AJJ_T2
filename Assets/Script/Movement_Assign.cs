using UnityEngine;

public class Movement_Assign : MonoBehaviour
{
    Movement move;
    All_moveable_gum_holder move_holder;
    void Awake()
    {
        move_holder = FindAnyObjectByType<All_moveable_gum_holder>();
        move = GetComponent<Movement>();

        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(Add_move);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Remove_move);
    }
    public void Add_move()
    {
        move_holder.Add_movealbe(move);
    }

    public void Remove_move()
    {
        Debug.Log("Removed Move");
        move_holder.Remove_moveable(move);
    }
}
