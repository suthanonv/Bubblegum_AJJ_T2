using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectTile : Grid_Collider
{
    LevelLoader lvl;
    [SerializeField] int lvlSelect;
    TMP_Text lvlName;
    bool isIn;
    private void Start()
    {
        lvl =  FindAnyObjectByType<LevelLoader>();
        lvlName = FindAnyObjectByType<lvlNameText>().gameObject.GetComponent<TMP_Text>();
    }

    private void Update()
    {
        if(isIn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                lvl.loadLevelSelectedScene(lvlSelect + SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    protected override void _OnEnter(MainComponent main)
    {
        if (main.GetComponent<StateControl<Bubble_Gum_State>>() != null)
        {
            isIn = true;
            lvlName.text = $"Level {lvlSelect + SceneManager.GetActiveScene().buildIndex}";
        }
    }

    protected override void _OnExit(MainComponent main)
    {
         isIn = false;
    }
}
