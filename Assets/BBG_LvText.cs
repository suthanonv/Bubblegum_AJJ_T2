using TMPro;
using UnityEngine;
public class BBG_LvText : MonoBehaviour
{

    [SerializeField] TextMeshPro m_TextMeshPro;

    LevelSelectTile m_LevelSelectTile;

    private void Awake()
    {
        UpdateLevelName(null);
    }

    public void UpdateLevelName(MoveAble_Tile _currentPlayerTile)
    {
        if (_currentPlayerTile == null) { m_TextMeshPro.enabled = false; return; }

        LevelSelectTile LvlTile = _currentPlayerTile.GetComponent<LevelSelectTile>();
        m_LevelSelectTile = LvlTile;
        if (LvlTile == null) { m_TextMeshPro.enabled = false; return; }



        if (LvlTile.CanPlay)
        {
            m_TextMeshPro.text = LvlTile.LvlName;
        }
        else
        {
            m_TextMeshPro.text = $"{LvlTile.LvlName} (Locked)";
        }

        m_TextMeshPro.enabled = true;







    }
}
