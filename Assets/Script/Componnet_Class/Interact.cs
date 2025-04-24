using UnityEngine;

public class Interact : MonoBehaviour
{
    Grid_Manager grid;

    [SerializeField] MainComponent mainComponent;

    private void Start()
    {
        grid = GameObject.FindAnyObjectByType<Grid_Manager>();
    }


    public void _Interact()
    {

        Vector2Int CurrentPos = mainComponent.Transform.currentTile_index;

        if (CurrentPos.x < 0) return;

        Debug.Log($"Current Tile Pos {CurrentPos}");


        foreach (Vector2Int all_direct in All_Direction.Directions)
        {

            Debug.Log($" Grid is  Null {grid == null}");

            if (grid.Get_Tile(all_direct + CurrentPos).gameObject.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {
                if ((tile.OcupiedObject == null)) continue;


                if (tile.OcupiedObject.FindComponnet_InChild<Area_interactable>() == null) continue;



                tile.OcupiedObject.FindComponnet_InChild<Area_interactable>().Interact();
            }
        }

    }
}
