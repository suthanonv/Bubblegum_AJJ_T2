using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectTile : Grid_Collider
{
    LevelLoader lvl;
    [SerializeField] int lvlSelect;

    public int LelSelect => lvlSelect;

    public string LelName => LvlName;

    TMP_Text lvlName;
    string LvlName;
    bool isIn;
    private void Start()
    {
        //lvl = FindAnyObjectByType<LevelLoader>();
        lvlName = this.transform.GetComponentInChildren<TMP_Text>();
        LvlName = SceneUtility.GetScenePathByBuildIndex(lvlSelect + SceneManager.GetActiveScene().buildIndex);
        lvlName.text = $"{LelSelect}";
        string[] sceneNamefull = LvlName.Split('/');
        int index = sceneNamefull.Length;
        LvlName = sceneNamefull[index - 1];
        string[] name = LvlName.Split(".");
        LvlName = name[0];
    }

    private void Update()
    {
        if (isIn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lvl = FindAnyObjectByType<LevelLoader>();
                lvl.loadLevelSelectedScene(lvlSelect + SceneManager.GetActiveScene().buildIndex);
            }
        }
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
