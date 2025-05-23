using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTile : Grid_Collider
{
    [SerializeField] private int lvlSelect;
    [SerializeField] private TMP_Text lvlNameText;
    [SerializeField] private SpriteRenderer lockUI;
    private string levelDisplayName;
    private bool isIn = false;


    public int LelSelect => lvlSelect;
    public string LvlName => levelDisplayName;
    TMP_Text lvlName;
    private LevelLoader level;
    Level_Info lvlInfo;
    MoveAble_Tile moveTile;

    bool Check(MainComponent something) => _canPlay;

    Level_Section section;
    [SerializeField] SceneAsset lvlSelect_Scene;

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (lvlSelect_Scene == null)
        {
            lvlSelect_Scene = SceneAssetUtils.GetSceneAssetByBuildIndex(lvlSelect);
            return;
        }
        lvlSelect = Level_Progress_Manager.GetBuildIndexByName(lvlSelect_Scene.name);
    }
#endif


    private void Start()
    {
        moveTile = this.GetComponent<MoveAble_Tile>();


        moveTile.Add_tile_Condition(Check);

        level = FindAnyObjectByType<LevelLoader>();
        if (level != null)
            level.OnBeginLoad += DisableSelf;
        string fullPath = SceneUtility.GetScenePathByBuildIndex(lvlSelect);


        levelDisplayName = System.IO.Path.GetFileNameWithoutExtension(fullPath);
        section = Level_Progress_Manager.Instance.GetSection(Level_Progress_Manager.GetBuildIndexByName(levelDisplayName));
        lvlInfo = section.GetLevel(lvlSelect);
        _canPlay = Set_canPlay();
        lvlNameText = this.GetComponentInChildren<TMP_Text>();
        lvlNameText.text = lvlSelect.ToString();
        string[] list = levelDisplayName.Split(' ');
        lvlNameText.text = list[1];


        if (_canPlay == false)
        {
            lvlNameText.color = Color.gray;
            lockUI.enabled = true;
        }

        if (_canPlay == true)
        {
            lockUI.enabled = false;
        }

        if (lvlInfo.IsClear)
        {
            lvlNameText.color = Color.green;

        }

    }



    bool isClicked = false;
    private void Update()
    {
        if (isIn && Input.GetKeyDown(KeyCode.Space))
        {
            if (_canPlay && !isClicked)
            {
                if (level == null)
                {
                    level = FindAnyObjectByType<LevelLoader>();
                    level.OnBeginLoad += DisableSelf;
                }

                Debug.Log("IM loadingggggggggg");
                isClicked = true;
                level.loadLevelSelectedScene(lvlSelect);
            }
        }
    }


    bool _canPlay;

    public bool CanPlay => _canPlay;


    void DisableSelf()
    {
        this.enabled = false;
    }
    private bool Set_canPlay()
    {
        if (Level_Progress_Manager.Instance.GetSection(Level_Progress_Manager.GetBuildIndexByName(levelDisplayName)) != null)
        {
            return Level_Progress_Manager.Instance.GetSection(Level_Progress_Manager.GetBuildIndexByName(levelDisplayName)).CanPlay;

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
