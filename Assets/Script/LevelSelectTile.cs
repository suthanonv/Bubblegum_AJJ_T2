using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTile : Grid_Collider
{
    [SerializeField] private int lvlSelect;
    [SerializeField] private TMP_Text lvlNameText;

    private string levelDisplayName;
    private bool isIn = false;

    
    public int LelSelect => lvlSelect;

    private void Start()
    {
        
        string fullPath = SceneUtility.GetScenePathByBuildIndex(lvlSelect + SceneManager.GetActiveScene().buildIndex);

        
        levelDisplayName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
    }

    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.Space))
        {
            LevelLoader lvl = FindAnyObjectByType<LevelLoader>();
            lvl.loadLevelSelectedScene(lvlSelect + SceneManager.GetActiveScene().buildIndex);
        }
    }

    protected override void _OnEnter(MainComponent main)
    {
        if (main.GetComponent<StateControl<Bubble_Gum_State>>() != null)
        {
            isIn = true;
            if (lvlNameText != null)
                lvlNameText.text = $"{levelDisplayName}";
        }
    }

    protected override void _OnExit(MainComponent main)
    {
        isIn = false;
        if (lvlNameText != null)
            lvlNameText.text = string.Empty;
    }
}
