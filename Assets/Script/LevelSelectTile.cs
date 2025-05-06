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
    private LevelLoader level;
    Level_Info lvlInfo;
    private void Start()
    {
        level = FindAnyObjectByType<LevelLoader>();
        string fullPath = SceneUtility.GetScenePathByBuildIndex(lvlSelect);


        levelDisplayName = System.IO.Path.GetFileNameWithoutExtension(fullPath);

        _canPlay = Set_canPlay();
        lvlInfo = Level_Progress_Manager.Instance.GetSection(levelDisplayName).GetLevel(levelDisplayName);

        lvlNameText = this.GetComponentInChildren<TMP_Text>();
        lvlNameText.text = lvlSelect.ToString();
        string[] list = levelDisplayName.Split(' ');
        lvlNameText.text = list[1];
        if (_canPlay == false)
            lvlNameText.color = Color.gray;
        else if (lvlInfo.IsClear)
            lvlNameText.color = Color.green;
    }


    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.Space))
        {
            if (_canPlay)
            {
                if (level == null) level = FindAnyObjectByType<LevelLoader>();

                level.loadLevelSelectedScene(lvlSelect);
            }

        }



    }


    bool _canPlay;

    public bool CanPlay => _canPlay;



    private bool Set_canPlay()
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

        }
    }

    protected override void _OnExit(MainComponent main)
    {
        isIn = false;

    }
}
