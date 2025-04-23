using UnityEngine;

public class box_setup : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.GetComponent<SpriteRenderer>().sortingOrder = Tile_SpriteOrder.GetSpriteOrder(OBjectType.StickAble);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
