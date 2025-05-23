using UnityEngine;

public class Movement_Controler : MonoBehaviour
{
    Movement move;
    void Awake()
    {
        move = GetComponent<Movement>();

        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_PreEnter_Listener(Add_move);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Remove_move);
    }
    public void Add_move()
    {
        InputMove_Holder.Instance.Add_movealbe(move);
    }

    public void Remove_move()
    {
        Debug.Log("Removed Move");
        InputMove_Holder.Instance.Remove_moveable(move);
    }
}
