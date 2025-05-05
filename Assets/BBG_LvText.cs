using TMPro;
using UnityEngine;
public class BBG_LvText : MonoBehaviour
{

    [SerializeField] TextMeshPro m_TextMeshPro;



    private void Awake()
    {
        UpdateLevelName(null);
    }

    public void UpdateLevelName(MoveAble_Tile _currentPlayerTile)
    {
        if (_currentPlayerTile == null) { m_TextMeshPro.enabled = false; return; }

        LevelSelectTile LvlTile = _currentPlayerTile.GetComponent<LevelSelectTile>();

        if (LvlTile == null) { m_TextMeshPro.enabled = false; return; }


        m_TextMeshPro.text = LvlTile.LelName;
        m_TextMeshPro.enabled = true;







    }
}
