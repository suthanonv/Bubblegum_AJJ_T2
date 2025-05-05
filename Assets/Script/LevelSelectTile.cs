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
    public string LvlName => levelDisplayName;
    TMP_Text lvlName;

    private void Start()
    {

        string fullPath = SceneUtility.GetScenePathByBuildIndex(lvlSelect + SceneManager.GetActiveScene().buildIndex);


        levelDisplayName = System.IO.Path.GetFileNameWithoutExtension(fullPath);

        _canPlay = CanPlay();
    }


    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.Space))
        {
            if (Level_Progress_Manager.Instance.GetSection(levelDisplayName).CanPlay)
            {
                LevelLoader lvl = FindAnyObjectByType<LevelLoader>();
                lvl.loadLevelSelectedScene(lvlSelect + SceneManager.GetActiveScene().buildIndex);
            }
        }
    }


    bool _canPlay;
    private bool CanPlay()
    {
        if (Level_Progress_Manager.Instance.GetSection(levelDisplayName) != null)
        {
            return Level_Progress_Manager.Instance.GetSection(levelDisplayName).CanPlay;

        }

        Debug.LogWarning($"No Section Found on {this.gameObject.name} GameObject");
        return false;

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
