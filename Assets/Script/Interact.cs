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

        Debug.Log("Begin Interacting after Movement");
        Vector2Int CurrentPos = mainComponent.Transform.currentTile_index;


        foreach (Vector2Int all_direct in All_Direction.Directions)
        {
            Debug.Log("Serching Direction");

            if (grid.Get_Tile(all_direct + CurrentPos).gameObject.TryGetComponent<MoveAble_Tile>(out MoveAble_Tile tile))
            {
                Debug.Log("Serching Object on Tile");
                if ((tile.OcupiedObject == null)) continue;
                Debug.Log($"Found Something... : {tile.OcupiedObject.gameObject.name}");

                Debug.Log($"is null == {tile.OcupiedObject.FindComponnet_InChild<Area_interactable>() == null}");
                Debug.Log($"is null == {tile.OcupiedObject.FindComponnet_InChild<Area_interactable>() is null}");

                if (tile.OcupiedObject.FindComponnet_InChild<Area_interactable>() == null) continue;


                Debug.Log($"Yeah! , found target to interact!! : {tile.OcupiedObject.gameObject.name}");

                tile.OcupiedObject.FindComponnet_InChild<Area_interactable>().Interact();
            }
        }
    }
}
