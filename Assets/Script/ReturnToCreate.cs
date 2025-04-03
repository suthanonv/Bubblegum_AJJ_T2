using UnityEngine;

public class ReturnToCreate : MonoBehaviour
{
    [SerializeField] LevelEditorInitializer ini;
    void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ini.Return();
        }
    }
}
