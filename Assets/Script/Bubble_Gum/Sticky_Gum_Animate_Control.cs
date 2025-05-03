using UnityEngine;

public class Sticky_Gum_Animate_Control : MonoBehaviour
{
    [SerializeField] MainComponent_Transform Main;
    [SerializeField] Animator animator;

    string State = "Base Layer.Stick.Stick";
    private void Awake()
    {
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);

    }




    void SetUp()
    {
        Main.FreezeRotation = true;
        Vector2Int _gum_Current_Direct = Main.Current_direction;

        animator.SetFloat("X", _gum_Current_Direct.x);
        animator.SetFloat("Y", _gum_Current_Direct.y);

        animator.Play(State, 0, 0);
        //   spriteRender.sortingLayerName = GetSpriteOrder(_gum_Current_Direct);

    }

    void Delete()
    {
        Main.FreezeRotation = false;
    }


    string GetSpriteOrder(Direction direct)
    {
        if (direct == Direction.Up) return "Elevated2InfrontGum";
        else return "ElevatedBehindGum";
    }

}

