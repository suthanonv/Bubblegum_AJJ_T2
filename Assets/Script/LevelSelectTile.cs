using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelSelectTile : Grid_Collider
{
    LevelLoader lvl;
    [SerializeField] int lvlSelect;

    public int LelSelect => lvlSelect;

    public string LelName => SceneManager.GetSceneByBuildIndex(lvlSelect).name;

    TMP_Text lvlName;
    string LvlName;
    bool isIn;
    private void Start()
    {
        //lvl = FindAnyObjectByType<LevelLoader>();
        lvlName = FindAnyObjectByType<lvlNameText>().gameObject.GetComponent<TMP_Text>();
        LvlName = SceneUtility.GetScenePathByBuildIndex(lvlSelect + SceneManager.GetActiveScene().buildIndex);
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
            lvlName.text = $"{LvlName}";
        }
    }

    protected override void _OnExit(MainComponent main)
    {
        isIn = false;
        lvlName.text = $"";
    }
}
