using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Sticky_Gum_Animate_Control : MonoBehaviour
{
    [SerializeField] List<StickyGum_SpriteList> Sprite_List = new List<StickyGum_SpriteList>();
    [SerializeField] MainComponent_Transform Main;

    SpriteRenderer spriteRender;


    private void Awake()
    {
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_EnterState_Listner(SetUp);
        this.transform.parent.GetComponent<StateBehaviour<Bubble_Gum_State>>().Add_ExitState_Listener(Delete);

        spriteRender = this.GetComponent<SpriteRenderer>();
        this.gameObject.SetActive(false);
    }




    void SetUp()
    {
        Main.FreezeRotation = true;
        this.gameObject.SetActive(true);
        Direction _gum_Current_Direct = Main.CurretionDirectionEnum;
        Debug.Log("Active");
        spriteRender.sprite = Sprite_List.FirstOrDefault(i => i.Direction == _gum_Current_Direct).GumSprite;
        spriteRender.sortingLayerName = GetSpriteOrder(_gum_Current_Direct);

    }

    void Delete()
    {
        Main.FreezeRotation = false;
        this.gameObject.SetActive(false);
    }


    string GetSpriteOrder(Direction direct)
    {
        if (direct == Direction.Up) return "Elevated2InfrontGum";
        else return "ElevatedBehindGum";
    }

}

[System.Serializable]
class StickyGum_SpriteList
{
    public Direction Direction;
    public Sprite GumSprite;
}