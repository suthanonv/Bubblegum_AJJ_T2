using System.Collections.Generic;
using UnityEngine;
public class Move_All : MonoBehaviour
{


    private void Start()
    {
        this.GetComponent<All_moveable_gum_holder>().Add_moveCall_Listener(MoveAll);
    }
    public void MoveAll(Vector2 Direction, List<Movement> moveGum)
    {

    }
}
